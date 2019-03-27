using Reddit.ViewModels.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Reddit.ViewModels.SubReddit
{
    public class FilterVM : BaseFilterVM<DataAccess.Entities.SubReddit>
    {
        [DisplayName("SubReddit Name:")]
        public string Name { get; set; }

        [DisplayName("Description:")]
        public string Description { get; set; }

        [DisplayName("Theme:")]
        public string Theme { get; set; }

        

        public override Expression<Func<DataAccess.Entities.SubReddit, bool>> GenerateFilter()
        {
            return i => (string.IsNullOrEmpty(Name) || i.Name.Contains(Name)) &&
                        (string.IsNullOrEmpty(Description) || i.Description.Contains(Description)) &&
                        (string.IsNullOrEmpty(Theme) || i.Theme.Contains(Theme));
                        
        }

    }
}