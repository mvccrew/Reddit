using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Reddit.ViewModels.Posts
{
    public class FilterVM
    {
        public int SubRedditId { get; set; }

        [DisplayName("Post Title")]
        public string PostTitle { get; set; }

        public Expression<Func<Post, bool>> GenerateFilter()
        {
            return i => (String.IsNullOrEmpty(PostTitle) || i.Title.Contains(PostTitle)) &&
                        (i.SubRedditId == SubRedditId);
        }
    }
}