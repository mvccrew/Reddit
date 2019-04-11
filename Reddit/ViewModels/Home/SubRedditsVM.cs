using DataAccess.Entities;
using Reddit.ViewModels.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Reddit.ViewModels.Home
{
    public class SubRedditsVM
    {
        public List<DataAccess.Entities.SubReddit> SubRedditsList { get;set; }
    }
}