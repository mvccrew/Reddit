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
    [AuthenticationFilter(AdminArea = true, RequiredKarma = int.MinValue)]
    public class UsersController : BaseController<User, UsersRepository, FilterVM, IndexVM, EditVM>
    {
        [HttpGet]
        public override ActionResult Edit(int? id)
        {
            User item = null;

            UsersRepository repo = new UsersRepository();
            item = id == null ? new User() : repo.GetById(id.Value);

            EditVM model = new EditVM();
            model.PopulateModel(item);
            PopulateEditVM(model);

            return PartialView("~/Views/Partials/_EditUser.cshtml", model);
        }

        [HttpPost]
        public override ActionResult Edit(EditVM model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("~/Views/Partials/_EditUser.cshtml", model);
            }

            UsersRepository repo = new UsersRepository();
            User item = new User();
            model.PopulateEntity(item);

            repo.Save(item);
            model.Id = item.Id;

            return Content("");
        }
    }
}