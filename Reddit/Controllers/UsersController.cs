using DataAccess.Entities;
using DataAccess.Repositories;
using Reddit.Filters;
using Reddit.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reddit.Controllers
{
    [AuthenticationFilter(AdminArea = true)]
    public class UsersController : BaseController<User, UsersRepository, FilterVM, IndexVM, EditVM>
    {
    }
}