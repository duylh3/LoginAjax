using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ideal.ViewModels
{
    public class LoginView
    {
        [Key]
        [Required(ErrorMessage = "required")]
        [Display(Name = "Login ID")]
        public string Username { get; set; }

        [Required(ErrorMessage = "required")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}