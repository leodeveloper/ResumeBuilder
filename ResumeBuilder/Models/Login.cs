using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models
{
    public class Login
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Invalid Username"), StringLength(100)]
        public string username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "invalid password"), StringLength(100)]

        public string password { get; set; }
    }




    public class LoginResponseModel
    {
        public string Token { get; set; }
        public bool ispasswordreset { get; set; }
    }

    //public class ClaimModel
    //{
    //    public string ClaimValue { get; set; }
    //    public string ClaimType { get; set; }
    //}

    public class ForgotPassword
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Invalid Email"), StringLength(100)]
        public string Email { get; set; }
    }


    public class ClaimModel
    {
        public string ClaimValue { get; set; }
        public string ClaimType { get; set; }
    }

}
