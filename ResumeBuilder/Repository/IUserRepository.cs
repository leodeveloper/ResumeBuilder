using Microsoft.AspNetCore.Http;
using ResumeBuilder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository 
{ 
    public interface IUserRepository
    {
        Task<Webresponse<IList<ClaimModel>>> VerifyToken(string token, HttpContext httpContext);
        public string GetTokenFromclaims(HttpContext httpContext);
        Task<string> GetToken(string userName);
    }
}