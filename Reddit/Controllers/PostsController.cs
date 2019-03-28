using DataAccess.Entities;
using DataAccess.Repositories;
using Reddit.ViewModels.Posts;
using Reddit.ViewModels.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Reddit.Controllers
{
    public class PostsController : Controller
    {
        public ActionResult Index(IndexVM model)
        {
            model.Pager = model.Pager ?? new PagerVM();
            model.Pager.Page = model.Pager.Page <= 0 ? 1 : model.Pager.Page;
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 10 : model.Pager.ItemsPerPage;

            model.Filter = model.Filter ?? new FilterVM();
            model.Filter.SubRedditId = model.SubRedditId;

            Expression<Func<Post, bool>> filter = model.Filter.GenerateFilter();

            PostsRepository repo = new PostsRepository();
            model.Posts = repo.GetAll(filter, model.Pager.Page, model.Pager.ItemsPerPage);
            model.Pager.PagesCount = (int)Math.Ceiling(repo.Count(filter) / (double)(model.Pager.ItemsPerPage));

            SubRedditsRepository subRedditsRepo = new SubRedditsRepository();
            model.SubReddit = subRedditsRepo.GetById(model.SubRedditId);

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id, int? subRedditId)
        {
            Post item = null;

            PostsRepository repo = new PostsRepository();

            item = id == null ? new Post() : repo.GetById(id.Value);

            EditVM model = item == null ? new EditVM() : new EditVM(item);

            if (model.Id <= 0)
            {
                model.SubRedditId = subRedditId.Value;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            PostsRepository repo = new PostsRepository();
            Post item = new Post();
            model.PopulateEntity(item);

            repo.Save(item);

            return RedirectToAction("Index", "Songs", new { PlayListId = item.SubRedditId });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            PostsRepository repo = new PostsRepository();

            Post item = repo.GetById(id);

            repo.Delete(item);

            return RedirectToAction("Index", "Songs", new { SubRedditId = item.SubRedditId });
        }
    }
}