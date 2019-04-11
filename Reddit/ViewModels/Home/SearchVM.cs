using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Reddit.ViewModels.Home
{
    public class SearchVM
    {
       public List<Post> Posts { get; set; }
       public List<DataAccess.Entities.SubReddit> SubReddits { get; set; }
        [Required(ErrorMessage = "Empty!")]
        public string Filter { get; set; }
    }
}