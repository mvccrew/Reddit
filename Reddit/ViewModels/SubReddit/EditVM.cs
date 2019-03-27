using Reddit.ViewModels.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataAccess.Entities;
using Reddit.Models;

namespace Reddit.ViewModels.SubReddit
{
    public class EditVM : BaseEditVM<DataAccess.Entities.SubReddit>

    {

        public int Id { get; set; }
        [DisplayName("SubReddit Name:")]
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }

        [DisplayName("Description:")]
        [Required(ErrorMessage = "This field is required")]
        public string Description { get; set; }

        [DisplayName("Theme:")]
        [Required(ErrorMessage = "This field is required")]
        public string Theme { get; set; }

        [DisplayName("Rules:")]
        [Required(ErrorMessage = "This field is required")]
        public string Rules { get; set; }


        public int UserId { get; set; }

        public override void PopulateModel(DataAccess.Entities.SubReddit item)
        {
            Id = item.Id;
            Name = item.Name;
            Description = item.Description;
            Theme = item.Theme;
            Rules = item.Rules;
            UserId = AuthenticationManager.LoggedUser.Id;
        }

        public override void PopulateEntity(DataAccess.Entities.SubReddit item)
        {
            item.Id = Id;
            item.Name = Name;
            item.Description = Description;
            item.Theme = Theme;
            item.Rules = Rules;
            AuthenticationManager.LoggedUser.Id = UserId;
            item.CreationDate = DateTime.Now;
        }
    }
}