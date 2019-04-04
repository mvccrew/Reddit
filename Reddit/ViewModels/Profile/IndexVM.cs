using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;
using Reddit.ViewModels.Share;

namespace Reddit.ViewModels.Profile
{
    public class IndexVM : BaseIndexVM<User, FilterVM>
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public List<Post> Posts { get; set; }

        public List<Comment> Comments { get; set; }
    }
}