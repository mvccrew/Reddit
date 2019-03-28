using DataAccess.Entities;
using DataAccess.Repositories;
using Reddit.Filters;
using Reddit.Models;
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
    [AuthenticationFilter]
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
        public ActionResult Edit(int? id)
        {
            Post item = null;

            PostsRepository repo = new PostsRepository();

            item = id == null ? new Post() : repo.GetById(id.Value);

            EditVM model = item == null ? new EditVM() : new EditVM(item);

            model.SubRedditsList = new List<SelectListItem>();

            SubRedditsRepository subRedditsRepo = new SubRedditsRepository();
            List<SubReddit> subscribedSubReddits = subRedditsRepo.GetMySubscribes(AuthenticationManager.LoggedUser.Id).ToList();

            foreach (SubReddit subReddit in subscribedSubReddits)
            {
                model.SubRedditsList.Add(
                    new SelectListItem()
                    {
                        Value = subReddit.Id.ToString(),
                        Text = subReddit.Name,
                        Selected = subReddit.Id == model.SelectedSubReddit
                    });
            }

            model.SubRedditId = model.SelectedSubReddit;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            SubRedditsRepository subRedditsRepo = new SubRedditsRepository();
            PostsRepository repo = new PostsRepository();

            Post item = new Post();
            model.PopulateEntity(item);

            repo.Save(item);

            return RedirectToAction("Index", "Posts", new { SubRedditId = item.SubRedditId });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            PostsRepository repo = new PostsRepository();

            Post item = repo.GetById(id);

            repo.Delete(item);

            return RedirectToAction("Index", "Posts", new { SubRedditId = item.SubRedditId });
        }
    }
}