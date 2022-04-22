using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumeBuilder.Dto;
using ResumeBuilder.Helper;
using ResumeBuilder.Models;
using ResumeBuilder.Repository;

namespace ResumeBuilder.Pages
{
    [Authorize]
    public class jobseekerModel : BasePageModel//PageModel
    {
        readonly ILogger<jobseekerModel> _logger;
        readonly IResumeRepository _iResumeRepository;
        readonly IOptions<MongoDBSettings> _appSettings;
        readonly IOptions<LookUpApiUrl> _appSettingsLookups;
        IMemoryCache _cache;
        public jobseekerModel(IMemoryCache memoryCache, ILogger<jobseekerModel> logger, IOptions<LookUpApiUrl> appSettingsLookups, IResumeRepository iResumeRepository, IOptions<MongoDBSettings> appSettings)
        {
            _logger = logger;
            _iResumeRepository = iResumeRepository;
            _appSettings = appSettings;
            _cache = memoryCache;
            _appSettingsLookups = appSettingsLookups;
        }

        [BindProperty]
        public Resume Resume { get; set; }
        //  public string AttachmentUniqueId { get { return Guid.NewGuid().ToString(); } }
        public string PdfDownloadURL { get { return _appSettingsLookups.Value.PdfApiUrl; } }

        public JobSeekerSearchModel jobSeekerSearchModel { get; set; }

        public void OnGet()
        {
            //try
            //{
            //    throw new Exception("Page Testing");
            //}
            //catch(Exception ex)
            //{
            //    _logger.LogError(ex,ex.Message);
            //}
        }

        public void OnPost(Resume jobSeekerResume)
        {
            if (ModelState.IsValid)
            {
                if (jobSeekerResume.Rid > 0)
                {
                    jobSeekerResume.LastModifiedUserID = User.Identity.Name;
                    jobSeekerResume.LastUpdateDate = System.DateTime.Now;
                    _iResumeRepository.UpdateResume(jobSeekerResume);
                }
                else
                {
                    jobSeekerResume.CreatedUserID = User.Identity.Name;                    
                    _iResumeRepository.InsertResume(jobSeekerResume);
                }
            }
            else
            {
                ModelState.AddModelError("", "");
            }
        }

      

        //public async Task<IActionResult> OnPostSearch(JobSeekerSearchModel jobSeekerSearchModel)
        //{
        //    return null;
        //}
        public async Task<IActionResult> OnPostFileUpload(IFormCollection form)
        {

            IFormFileCollection files = Request.Form.Files;
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string s = Convert.ToBase64String(fileBytes);
                        Webresponse<string> webresponse = await _iResumeRepository.Insert(new Resume_Attachment { ResumeAttachmentBase64String = s, UnqiueId = form["UniqueAttachmentID"], FileName = file.FileName });
                        if(webresponse.status != APIStatus.success)
                        {
                            _logger.LogError("Error in file upload on mongo db");
                        }
                    }
                }
            }
            return null;
        }
    }
}
