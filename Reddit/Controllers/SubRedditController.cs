using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Repositories;
using Reddit.Filters;
using Reddit.Models;
using Reddit.ViewModels.SubReddit;

namespace Reddit.Controllers
{
    public class SubRedditController : BaseController<SubReddit, SubRedditsRepository, FilterVM, IndexVM, EditVM >
    {
        [AuthenticationFilter(RequiredKarma = 0)]
        [HttpGet]
        public override ActionResult Edit(int? id)
        {
            SubReddit item = null;

            SubRedditsRepository repo = new SubRedditsRepository();
            item = id == null ? new SubReddit() : repo.GetById(id.Value);

            EditVM model = new EditVM();
            model.PopulateModel(item);
            PopulateEditVM(model);

            return View(model);
        }

        [AuthenticationFilter(RequiredKarma = 0)]
        [HttpPost]
        public override ActionResult Edit(EditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            SubRedditsRepository repo = new SubRedditsRepository();
            SubReddit item = new SubReddit();
            model.PopulateEntity(item);

            repo.Save(item);

            model.Id = item.Id;

            // като създадеш събреддит, автоматично ставаш мод и се събскрайбваш за него
            repo.AddAdminToSubReddit(model.Id, model.UserId);
            repo.Subscribe(model.Id, model.UserId);

            return RedirectToAction("Index", "SubReddit");
        }

        [AuthenticationFilter(RequiredKarma = int.MinValue)]
        public ActionResult Subscribe(int id)
        {
            SubRedditsRepository repo = new SubRedditsRepository();

            repo.Subscribe(id, AuthenticationManager.LoggedUser.Id);

            return RedirectToAction("Index", "Home");
        }

    }
}