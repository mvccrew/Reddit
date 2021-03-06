﻿using Reddit.ViewModels.Share;
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

        public int UserId { get; set; }

        [DisplayName("Name:")]
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }

        [DisplayName("Description:")]
        [Required(ErrorMessage = "This field is required")]
        public string Description { get; set; }

        [DisplayName("Theme:")]
        [Required(ErrorMessage = "This field is required")]
        public string Theme { get; set; }

        public List<Rule> Rules { get; set; }

        public override void PopulateModel(DataAccess.Entities.SubReddit item)
        {
            Id = item.Id;
            UserId = item.UserId;
            Name = item.Name;
            Description = item.Description;
            Theme = item.Theme;
        }

        public override void PopulateEntity(DataAccess.Entities.SubReddit item)
        {
            item.Id = Id;
            item.UserId = UserId;
            item.Name = Name;
            item.Description = Description;
            item.Theme = Theme;
            item.CreationDate = DateTime.Now;
        }
    }
}