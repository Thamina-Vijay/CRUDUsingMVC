using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CRUDUsingMVC.Models
{
    public class StudentModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Name e.g. John Doe")]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Compare("Email")]
        public string RetypeEmail { get; set; }

        [DisplayName("Phone Number")]
        public string Phone { get; set; }

        public byte[] Image { get; set; }


    }
}
    
