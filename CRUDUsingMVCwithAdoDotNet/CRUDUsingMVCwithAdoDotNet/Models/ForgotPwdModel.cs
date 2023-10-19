using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CRUDUsingMVC.Models
{
    public class ForgotPwdModel
    {
        [Display(Name = "UserName")]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "NewPassword")]
        public string NewPassword { get; set; }

        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}