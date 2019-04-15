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
        public ActionResult MakeAdmin(int subRedditId, int userId)
        {
            SubRedditsRepository repo = new SubRedditsRepository();
            repo.AddAdminToSubReddit(subRedditId, userId);

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult BanUser(int subRedditId, int userId)
        {
            SubRedditsRepository repo = new SubRedditsRepository();
            repo.BanUser(subRedditId, userId);
            repo.UnSubscribe(subRedditId, userId);

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult MuteUser(int subRedditId, int userId)
        {
            SubRedditsRepository repo = new SubRedditsRepository();
            repo.MuteUser(subRedditId, userId);

            return Redirect(Request.UrlReferrer.ToString());
        }

        [AuthenticationFilter(RequiredKarma = int.MinValue)]
        public ActionResult Subscribe(int id)
        {
            
            SubRedditsRepository repo = new SubRedditsRepository();
            SubReddit sub = repo.GetById(id);
            if (AuthManager.LoggedUser.SubscribedToSubReddits.Any(m => m.Id==id)
                && sub.SubscribedUsers.Any(m => m.Id==AuthManager.LoggedUser.Id))
            {
                repo.UnSubscribe(id, AuthManager.LoggedUser.Id);
            }
            else
            {
                repo.Subscribe(id, AuthManager.LoggedUser.Id);
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult UnSubscribe(int id)
        {
            SubRedditsRepository repo = new SubRedditsRepository();

            repo.UnSubscribe(id,AuthManager.LoggedUser.Id);

            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}