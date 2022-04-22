using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumeBuilder.Models;

namespace ResumeBuilder.Pages
{
    public class unauthorizeModel : PageModel
    {

        private readonly ILogger<LogoutModel> _logger;
        private readonly IOptions<AppSettings> _appsetting;
        
        [BindProperty]
        public string samlloginurl { get; set; }

        public unauthorizeModel(IOptions<AppSettings> appsetting, ILogger<LogoutModel> logger)
        {
            _appsetting = appsetting;
            samlloginurl = _appsetting.Value.samlLogURl;
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
