using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Helper
{
    public class BasePageModel : PageModel
    {
        public IActionResult OnPostSetLanguageAsync(string culture, string returnUrl)
        {
            try
            {

                Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    if (Convert.ToString(returnUrl).Trim() == "/")
                        return RedirectToPage("jobseeker");
                    else
                    {
                        if (!string.IsNullOrEmpty(returnUrl))
                            return RedirectPermanent("jobseeker");
                        //Below is not required as permanet redirect take cares of route value
                        else if (returnUrl.Trim().ToLower().Contains("jobseeker"))
                        {
                            //two case : 1 : with route
                            string[] sUrlarray = returnUrl.Split("/");
                            if (sUrlarray.Length == 3)
                            {
                                return RedirectToPage(Convert.ToString(Convert.ToString(sUrlarray[1])), new { rid = Convert.ToString(sUrlarray[2]) });
                            }
                            //2 : without route
                            else
                                return RedirectToPage(Convert.ToString("jobseeker"));
                        }
                        else
                            return RedirectToPage(Convert.ToString("jobseeker"));
                    }

                }

                else
                    return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                Log.Error("BasePageModel:OnPostSetLanguageAsync :: Error message => {error}", ex.Message);
                return RedirectToPage("Index");
            }

        }
    }
}
