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
        [Required(ErrorMessage = "This field is Required!")]
        public string Email { get; set; }


        [DisplayName("Username:")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Username { get; set; }
        [DisplayName("Password:")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Password { get; set; }
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string RetypePassword { get; set; }
        [DisplayName("First Name:")]
        [Required(ErrorMessage = "This field is Required!")]
        public string FirstName { get; set; }
        [DisplayName("Last Name:")]
        [Required(ErrorMessage = "This field is Required!")]
        public string LastName { get; set; }
    }
}