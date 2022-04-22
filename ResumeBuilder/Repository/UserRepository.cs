using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ResumeBuilder.Models;
using ResumeBuilder.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using static ResumeBuilder.Helper.UserManagerEnum;
using ResumeBuilder.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;

namespace ResumeBuilder.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly IOptions<AppSettings> _appsetting;
        private readonly IConfiguration configuration;

        public UserRepository(ILogger<UserRepository> logger, IOptions<AppSettings> appsetting, IConfiguration configuration)
        {
            _logger = logger;
            _appsetting = appsetting;
            this.configuration = configuration;
        }

        public async Task<string> GetToken(string userName)
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    var getTokenResponse = await _httpClient.PostAsJsonAsync<WindowsLoginModel>(_appsetting.Value.AuthenticationAPI + "/AuthActiveDirectory/Windowslogin", new WindowsLoginModel { Username = userName });
                    if (getTokenResponse.IsSuccessStatusCode)
                    {
                        Webresponse<LoginResponseModel> webresponse = JsonConvert.DeserializeObject<Webresponse<LoginResponseModel>>(await getTokenResponse.Content.ReadAsStringAsync());
                        if (webresponse.status == APIStatus.success)
                        {
                            return webresponse.data.Token;
                        }
                        else
                            _logger.LogError($"UserRepository GetToken failed for this user {userName}");
                    }
                    else
                    {
                        _logger.LogError($"UserRepository GetToken failed for this user {userName}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"UserRepository GetToken failed for this user {userName} :::::: {ex.Message}");
            }
            return null;
        }

        public async Task<Webresponse<IList<ClaimModel>>> VerifyToken(string token, HttpContext httpContext)
        {
            Webresponse<IList<ClaimModel>> webresponse = new Webresponse<IList<ClaimModel>>();
            try
            {                
                using (var _httpClient = new HttpClient())
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var VerifyResponse = await _httpClient.GetAsync(_appsetting.Value.AuthenticationAPI + "/Auth/Verify");
                    //Get Claims
                    if (VerifyResponse.IsSuccessStatusCode)
                    {
                        webresponse = JsonConvert.DeserializeObject<Webresponse<IList<ClaimModel>>>(await VerifyResponse.Content.ReadAsStringAsync());
                        
                        //ClaimModel claimUser = webresponse.data.FirstOrDefault(z => z.ClaimType == ClaimTypes.Name);
                        //if (!claimUser.ClaimValue.Equals(userName))
                        //{
                        //    _logger.LogError($"Token username is different from username provided in the header");                           
                        //}
                        ClaimModel claimEmail = webresponse.data.FirstOrDefault(z => z.ClaimType == ClaimTypes.Email);
                        ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                        foreach (var claim in webresponse.data)
                        {
                            identity.AddClaim(new Claim(claim.ClaimType, claim.ClaimValue));
                        }
                        httpContext.User.AddIdentity(identity);
                        await SignIn(identity, claimEmail.ClaimValue, httpContext, token);
                    }
                    else
                    {
                        _logger.LogError($"Token varification failed for user");
                        webresponse.status = APIStatus.error;
                    }                        
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"verfiyToken is failed due to this exception {ex.Message}");
                webresponse.status = APIStatus.error;
            }

            return webresponse;


        }

        /// <summary>
        /// Get contact detial and signin
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        private async Task<bool> SignIn(ClaimsIdentity identity, string userEmail, HttpContext httpContext, string token)
        {
            try
            {
                using (HttpClient _httpClient = new HttpClient())
                {
                    Webresponse<Contact> WebResponse = await _httpClient.GetFromJsonAsync<Webresponse<Contact>>(_appsetting.Value.EntitiesAPI + "/GetContactbyEmail?contactEmail=" + userEmail);
                    identity.AddClaim(new Claim(ClaimEnum.contactId.ToString(), WebResponse.data.Id.ToString()));
                    identity.AddClaim(new Claim(ClaimEnum.companyId.ToString(), WebResponse.data.Companyid.ToString()));
                    identity.AddClaim(new Claim(ClaimEnum.token.ToString(), token));
                    identity.AddClaim(new Claim(ClaimEnum.HCUserId.ToString(), WebResponse.data?.hcuser_rid == null?string.Empty: WebResponse.data?.hcuser_rid.ToString())) ;
                    var principal = new ClaimsPrincipal(identity);
                     await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = false });
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"SignIn failed, please check the contact API email is not working, exception {ex.Message}");
            }
            return false;
        }

        public string GetTokenFromclaims(HttpContext httpContext)
        {
            try
            {               
                //Filter specific claim
                return httpContext.User.Claims?.FirstOrDefault(x => x.Type.Equals("token", StringComparison.OrdinalIgnoreCase))?.Value;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"IUserRepository ::::: GetTokenFromclaims ::::: {ex.Message}");
                return string.Empty;
            }           
        }

        /// <summary>
        /// This function return the token for the passed user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<string> GetTokenForWindowsUser(string userName)
        {

            try
            {
                using (HttpClient _httpClient = new HttpClient())
                {
                    WindowsTokenUserName windowsTokenUserName = new WindowsTokenUserName { username = userName.Split('\\').Last() };
                    var url = configuration["AppSettings:AuthenticationAPI"];
                    var response = await _httpClient.PostAsJsonAsync<WindowsTokenUserName>(url + "/AuthActiveDirectory/Windowslogin", windowsTokenUserName);
                    if (response.IsSuccessStatusCode)
                    {
                        Webresponse<LoginResponseModel> webresponse = await response.Content.ReadFromJsonAsync<Webresponse<LoginResponseModel>>();
                        if (webresponse.status == APIStatus.success)
                            return webresponse.data.Token;
                    }
                    return string.Empty;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"IUserRepository ::::: GetTokenFromclaims ::::: {ex.Message}");
                return string.Empty;
            }
        }

    }

    public class WindowsTokenUserName
    {
        public string username { get; set; }
    }
}
