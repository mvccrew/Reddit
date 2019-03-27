using DataAccess.Entities;
using Reddit.ViewModels.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Reddit.ViewModels.Profile
{
    public class FilterVM : BaseFilterVM<User>
    {
        public override Expression<Func<User, bool>> GenerateFilter()
        {
            return i => i.Id == i.Id;
        }
    }
}