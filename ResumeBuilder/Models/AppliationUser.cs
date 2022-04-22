using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models
{
    public class ApplicationUser : IdentityUser
    {

        //When the next time user login he must needs to resert the password
        public bool ispasswordreset { get; set; }


    }
}
