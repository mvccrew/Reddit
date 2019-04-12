using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Reddit.ViewModels.Home
{
    public class RegisterVM
    {
        [EmailAddress]
        [DisplayName("Email:")]
        [Required(ErrorMessage = "This field is required!")]
        public string Email { get; set; }
        
        [DisplayName("Username:")]
        [Required(ErrorMessage = "This field is required!")]
        public string Username { get; set; }

        [DisplayName("Password:")]
        [Required(ErrorMessage = "This field is required!")]
        public string Password { get; set; }

        [DisplayName("Retype password:")]
        [Required(ErrorMessage = "This field is required!")]
        [Compare("Password")]
        public string RetypePassword { get; set; }

        [DisplayName("First name:")]
        [Required(ErrorMessage = "This field is required!")]
        public string FirstName { get; set; }

        [DisplayName("Last name:")]
        [Required(ErrorMessage = "This field is required!")]
        public string LastName { get; set; }
    }
}