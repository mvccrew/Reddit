using DataAccess.Entities;
using Reddit.ViewModels.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reddit.ViewModels.SubReddit
{
    public class IndexVM : BaseIndexVM<DataAccess.Entities.SubReddit, FilterVM>
    {
        public int UserId { get; set; }
        public User User { get; set; }
    }
}