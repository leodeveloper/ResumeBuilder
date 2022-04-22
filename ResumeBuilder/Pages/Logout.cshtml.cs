using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ResumeBuilder.Models;
using ResumeBuilder.Repository;

namespace ResumeBuilder.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly ILogger<LogoutModel> _logger;
        private readonly IOptions<AppSettings> _appsetting;
        private readonly IUserRepository _iUserRepository;
        [BindProperty]
        public string samlloginurl { get; set; }

        public LogoutModel(IOptions<AppSettings> appsetting, IUserRepository iUserRepository, ILogger<LogoutModel> logger)
        {
            _appsetting = appsetting;
            samlloginurl = _appsetting.Value.samlLogURl;
            _iUserRepository = iUserRepository;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {        

            await LogOut(_iUserRepository.GetTokenFromclaims(HttpContext));
            return Page();
        }

        public async Task<bool> LogOut(string token)
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme,  new AuthenticationProperties { IsPersistent = false });
                using (var _httpClient = new HttpClient())
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var VerifyResponse = await _httpClient.GetAsync(_appsetting.Value.AuthenticationAPI + "/Auth/Logout");
                    if (VerifyResponse.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        _logger.LogError("LogOut API token Logout failed::: ");
                        return false;
                    }
                }
            }
            catch(Exception ex) 
            {
                _logger.LogError("LogOut ::: " + ex.Message);
                return false;
            }
            
        }
    }
}
