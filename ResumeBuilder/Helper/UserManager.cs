using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using ResumeBuilder.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ResumeBuilder.Helper
{

    public class UserManagerEnum
    {
        public enum ClaimEnum
        {
            contactId,
            companyId,
            token,
            HCUserId,
        }
    }

    public static class UserHelper
    {
        public static string GetClaimValue(HttpContext httpContext, string claimname)
        {
            string claimValue = "";
            try
            {
                claimValue = httpContext.User.Claims.FirstOrDefault(a => a.Type.EndsWith(Convert.ToString(claimname).Trim().ToLower())).Value;
            }
            catch (Exception ex)
            {
                Log.Error("UserHelper:GetClaimValue :: Error message => {error}", ex.Message);
                httpContext.Response.Redirect("/Login?handler=Logout");
            }

            return claimValue;
        }

    }

    public class UserManager
    {
        private IEnumerable<Claim> GetUserClaims(ApplicationUser user)
        {
            
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)));
           // claims.Add(new Claim(ClaimTypes.Name, user.name_en));
          //  claims.Add(new Claim(ClaimTypes.Email, user.email));
            claims.AddRange(this.GetUserRoleClaims(user));
            return claims;
        }

        private IEnumerable<Claim> GetUserRoleClaims(ApplicationUser user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)));
           // claims.Add(new Claim(ClaimTypes.Role, Convert.ToString(user.per)));
            return claims;
        }
    }
}
