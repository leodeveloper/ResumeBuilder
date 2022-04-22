using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ResumeBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Helper
{
    public class RoleHelper
    {
        private readonly IOptions<JobSeekerRoles> _optionsJobSeekerRoles;
        private static string adminRoles, userRoles;
        public RoleHelper(IOptions<JobSeekerRoles> optionsJobSeekerRoles)
        {
            _optionsJobSeekerRoles = optionsJobSeekerRoles;
            adminRoles = _optionsJobSeekerRoles.Value.AdminRole;
            userRoles = _optionsJobSeekerRoles.Value.UserRole;
        }

        //private static IOptions<JobSeekerRoles> _optionsJobSeekerRoles
        //{

        //  // get { return HttpContext.RequestServices.GetService(typeof(IOptions<JobSeekerRoles>)};
        //    // DependencyResolver could be any DI container here.
        //    get { return DependencyResolver.Resolve<IOptions<JobSeekerRoles>>(); }
        //}

        public static string GetAdminRoles()
        {
            return adminRoles;
        }

        public static string GetUserRoles()
        {
            return userRoles;
        }
    }

    public static class JobSeekerRolesSettings
    {
        public static JobSeekerRoles JobSeekerRolesSetting { get; set; } = new JobSeekerRoles();
    }
}
