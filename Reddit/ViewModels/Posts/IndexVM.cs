using DataAccess.Entities;
using Reddit.ViewModels.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reddit.ViewModels.Posts
{
    public class IndexVM 
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public PagerVM Pager { get; set; }

        public FilterVM Filter { get; set; }

        public List<Post> Posts { get; set; }

        public int SubRedditId { get; set; }
        public DataAccess.Entities.SubReddit SubReddit { get; set; }
    }
}