﻿using DataAccess.Entities;
using Reddit.ViewModels.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Reddit.ViewModels.Profile
{
    public class EditVM : BaseEditVM<User>
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public int Karma { get; set; }

        public bool IsAdmin { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Password: ")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Retype password: ")]
        [Compare("Password", ErrorMessage = "Both passwords must match!")]
        public string RetypePassword { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("First name: ")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Last name: ")]
        public string LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Email: ")]
        public string Email { get; set; }

        public override void PopulateEntity(User item)
        {
            item.Id = Id;
            item.Username = Username;
            item.Password = Password;
            item.FirstName = FirstName;
            item.LastName = LastName;
            item.Email = Email;
            item.Karma = Karma;
            item.IsAdmin = IsAdmin;
        }

        public override void PopulateModel(User item)
        {
            Id = item.Id;
            Username = item.Username;
            Password = item.Password;
            FirstName = item.FirstName;
            LastName = item.LastName;
            Email = item.Email;
            Karma = item.Karma;
            IsAdmin = item.IsAdmin;
        }
    }
}