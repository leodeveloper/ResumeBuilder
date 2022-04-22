using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using ResumeBuilder.Dto;
using ResumeBuilder.Helper;
using ResumeBuilder.Models;
using ResumeBuilder.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ResumeBuilder.Pages
{
    //[Authorize]
    public class IndexModel : BasePageModel
    {

        readonly ILogger<IndexModel> _logger;
        readonly IOptions<LookUpApiUrl> _appSettingsLookups;      

        private readonly IUserRepository _iUserRepository;
        public IndexModel(IUserRepository iUserRepository, ILogger<IndexModel> logger, IOptions<LookUpApiUrl> appSettingsLookups)
        {
            _logger = logger;
            _appSettingsLookups = appSettingsLookups;
            _iUserRepository = iUserRepository;
        }

        public async Task<IActionResult> OnGet(string user_name, string user_token)
        {
            try
            {
#if DEBUG
                user_name = "t0145";
#endif

                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToPage("jobseeker");
                }
#if DEBUG
                string userNameWithoutDomain = user_name.Split('\\').Last().ToLower();
                user_token = await _iUserRepository.GetToken(userNameWithoutDomain);
#endif
                if (string.IsNullOrEmpty(user_token))
                {
                    return RedirectToPage("unauthorize");
                }


                Webresponse<IList<ClaimModel>> response = await _iUserRepository.VerifyToken(user_token, HttpContext);
                if (response.status == APIStatus.success)
                {
                    return RedirectToPage("jobseeker");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }            
            return RedirectToPage("unauthorize");
        }
    }

    //[Authorize]
    //public class IndexModel : BasePageModel 
    //{
    //    readonly ILogger<IndexModel> _logger;
    //    readonly IOptions<LookUpApiUrl> _appSettingsLookups;
    //    private string _token;
    //    private string _username;

    //    private readonly IUserRepository _iUserRepository;
    //    public IndexModel(IUserRepository iUserRepository, ILogger<IndexModel> logger, IOptions<LookUpApiUrl> appSettingsLookups)
    //    {
    //        _logger = logger;           
    //        _appSettingsLookups = appSettingsLookups;
    //        _iUserRepository = iUserRepository;
    //    }

    //    [BindProperty]
    //    public Resume Resume { get; set; }
    //  //  public string AttachmentUniqueId { get { return Guid.NewGuid().ToString(); } }
    //    public string PdfDownloadURL { get { return _appSettingsLookups.Value.PdfApiUrl; } }

    //    public JobSeekerSearchModel jobSeekerSearchModel { get; set; }       

    //    public async Task<IActionResult> OnGetAsync(string user_name, string user_token)
    //    {
    //        if (string.IsNullOrEmpty(user_name) && string.IsNullOrEmpty(user_token))
    //        {
    //            HttpContext.Request.Headers.TryGetValue("X-Username", out StringValues username);
    //            _username = username;
    //            HttpContext.Request.Headers.TryGetValue("X-Authorization", out StringValues token);
    //            _token = token;
    //        }
    //        else
    //        {
    //            _username = user_name;
    //            _token = user_token;
    //        }

    //        if (_username == null || _token == null)
    //        {
    //            _logger.LogError("SSO:GET:Username and token is missing in the request");
    //            return RedirectToPage("/login");
    //        }           

    //        if(await AuthenticationService(_username, _token))
    //        {
    //            return RedirectToPage("jobseeker");
    //        }
    //        return RedirectToPage("login");           
    //    }

    //    public async Task<bool> AuthenticationService(string userName, string token)
    //    {
    //        using (var _httpClient = new HttpClient())
    //        {
    //            var isValid = await _iUserRepository.VerifyToken(token, userName, HttpContext);
    //            return isValid;
    //        }
    //    }      
    //}
}
