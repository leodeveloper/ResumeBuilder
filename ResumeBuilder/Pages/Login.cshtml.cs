using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Serilog;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Json;
using System.Xml;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using Formatting = Newtonsoft.Json.Formatting;
using Microsoft.Extensions.Logging;
using ResumeBuilder.Models;
using ResumeBuilder.Repository;

namespace ResumeBuilder.Pages
{

    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly IOptions<AppSettings> _appsetting;
        private readonly IUserRepository _iUserRepository;
        //this will bind post form
        [BindProperty] public Login login { get; set; } 

        public LoginModel(IOptions<AppSettings> appsetting, ILogger<LoginModel> logger, IUserRepository iUserRepository)
        {          
            _appsetting = appsetting;
            _logger = logger;
            _iUserRepository = iUserRepository;
        }
         
        public void OnGet()
        { 
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isVerified =  await AuthenticationService(login.username, login.password);
                    if (isVerified)
                    {
                        //if (!string.IsNullOrEmpty(HttpContext.Request.Query["ReturnUrl"]))
                        //{
                        //    if (Convert.ToString(HttpContext.Request.Query["ReturnUrl"]).Trim() == "/")
                        //        return RedirectToPage("/Index");
                        //    else
                        //        return RedirectToPage(Convert.ToString(HttpContext.Request.Query["ReturnUrl"]));
                        //}
                        //else
                            return RedirectToPage("/jobseeker");
                    }
                    else
                        ModelState.AddModelError("invalidcredentials", "username or password is invalid");
                }
                else                
                    ModelState.AddModelError("invalidcredentials", "username or password is blank");     
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("invalidcredentials", "username or password is invalid");
                _logger.LogError("LoginModel:OnPostAsync :: Error message => {error}", ex.Message);                
            }
            return Page();
        }

        public async Task<bool> AuthenticationService(string userName, string password)
        {
            using(var _httpClient = new HttpClient())
            {
                Dictionary<string, string> BugData = new Dictionary<string, string>
            {
                { "username", userName },
                { "password", password }
            };
                string Json = JsonConvert.SerializeObject(BugData, Formatting.Indented);
                HttpContent content = new StringContent(Json, Encoding.UTF8, "application/json");
                var loginResponse = await _httpClient.PostAsync(_appsetting.Value.AuthenticationAPI + "/Auth/login", content);
                if (loginResponse.IsSuccessStatusCode)
                {
                    Webresponse<LoginResponseModel> loginResponseModel  =await loginResponse.Content.ReadFromJsonAsync<Webresponse<LoginResponseModel>>();
                   // var isValid = await _iUserRepository.VerifyToken(loginResponseModel.data.Token, userName, HttpContext);
                  //  return isValid;
                }
            }          

            return false;
        }

        public async Task<IActionResult> OnGetLogoutAsync()
        {
            await HttpContext.SignOutAsync();
            //return RedirectToPage("Login");
            return RedirectToPage(_appsetting.Value.LogoutUrl);
        }
    }
}
