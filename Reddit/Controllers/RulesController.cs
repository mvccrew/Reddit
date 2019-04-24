using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Entities;
using DataAccess.Repositories;
using Reddit.ViewModels.Rules;

namespace Reddit.Controllers
{
    public class RulesController : Controller
    {
        [HttpGet]
        public ActionResult Add(int? id, int? SubRedditId)
        {
            Rule item = null;

            RulesRepository repo = new RulesRepository();

            item = id == null ? new Rule() : repo.GetById(id.Value);

            EditVM model = item == null ? new EditVM() : new EditVM(item);
            if (model.Id <= 0)
            {
                model.SubRedditId = SubRedditId.Value;
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult Add(EditVM model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("~/Views/Partials/Edits/_EditComment.cshtml", model);
            }

            RulesRepository repo = new RulesRepository();
            Rule item = new Rule();
            model.PopulateEntity(item);

            repo.Save(item);

            return RedirectToAction("Index", "Posts", new { SubRedditId = item.SubRedditId });
        }

    }
}