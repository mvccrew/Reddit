using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Reddit.ViewModels.Home
{
    public class LoginVM
    {
        [DisplayName("Username:")]
        [Required(ErrorMessage = "This field is required!")]
        public string Username { get; set; }
        [DisplayName("Password:")]
        [Required(ErrorMessage = "This field is required!")]
        public string Password { get; set; }
    }
}