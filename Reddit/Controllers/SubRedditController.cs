using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Repositories;
using Reddit.ViewModels.SubReddit;

namespace Reddit.Controllers
{
    public class SubRedditController : BaseController<SubReddit, SubRedditsRepository, FilterVM, IndexVM, EditVM >
    {
      
    }
}