using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ResumeBuilder.Helper
{
    //public class ClaimsTransformer //: IClaimsTransformation
    //{
    //    // Can consume services from DI as needed, including scoped DbContexts
    //    //public ClaimsTransformer(IHttpContextAccessor httpAccessor) { }
    //    //public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal p)
    //    //{
    //    //    ClaimsIdentity claimsIdentity = new ClaimsIdentity();
    //    //   // claimsIdentity.AddClaim();
    //    //    p.AddIdentity(claimsIdentity);
    //    //    return Task.FromResult(p);
    //    //}
    //}

    public class CustomClaimsPrincipal : ClaimsPrincipal
    {
        public CustomClaimsPrincipal(IPrincipal principal) : base(principal)
        { }

        public override bool IsInRole(string roles)
        {
            foreach(var role in roles.Split(','))
            {
                if (base.IsInRole(role))
                    return true;
            }
            return false;
             
        }
    }

    public class ClaimsTransformer : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var customPrincipal = new CustomClaimsPrincipal(principal) as ClaimsPrincipal;
            return Task.FromResult(customPrincipal);
        }
    }
}
