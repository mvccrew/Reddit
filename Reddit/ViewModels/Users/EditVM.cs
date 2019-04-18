using DataAccess.Entities;
using Reddit.ViewModels.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reddit.ViewModels.Users
{
    public class EditVM : BaseEditVM<User>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Username: ")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Password: ")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("First name: ")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Last name: ")]
        public string LastName { get; set; }

        [DisplayName("Administrator: ")]
        public bool IsAdmin { get; set; }

        public override void PopulateEntity(User item)
        {
            item.Id = Id;
            item.Username = Username;
            item.Password = Password;
            item.FirstName = FirstName;
            item.LastName = LastName;
            item.IsAdmin = IsAdmin;
        }

        public override void PopulateModel(User item)
        {
            Id = item.Id;
            Username = item.Username;
            Password = item.Password;
            FirstName = item.FirstName;
            LastName = item.LastName;
            IsAdmin = item.IsAdmin;
        }
    }
}