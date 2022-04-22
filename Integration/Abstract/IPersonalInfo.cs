using Integration.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.Abstract
{
    public interface IPersonalInfo
    {
        [Post("/GradeAPI/api/Grade/verifyemiratesID")]
        Task<PersonalInfo> GetEmaritesByEmirateID(string emaritesID);
    }
}
