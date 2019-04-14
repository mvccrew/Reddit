using DataAccess.Entities;
using Reddit.ViewModels.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Reddit.ViewModels.Users
{
    public class FilterVM : BaseFilterVM<User>
    {
        [DisplayName("Username")]
        public string Username { get; set; }

        [DisplayName("First name")]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        public string LastName { get; set; }

        public override Expression<Func<User, bool>> GenerateFilter()
        {
            return i => (String.IsNullOrEmpty(Username) || i.Username.Contains(Username)) &&
                        (String.IsNullOrEmpty(FirstName) || i.FirstName.Contains(FirstName)) &&
                        (String.IsNullOrEmpty(LastName) || i.LastName.Contains(LastName));
        }
    }
}