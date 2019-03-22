using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reddit.ViewModels.Share
{
    public abstract class BaseIndexVM<E, F>
        where E : BaseEntity, new()
        where F : BaseFilterVM<E>, new()
    {
        public PagerVM Pager { get; set; }
        public F Filter { get; set; }
        public List<E> Items { get; set; }
    }
}