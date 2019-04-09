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
    [AuthenticationFilter(RequiredKarma = int.MinValue)]
    public class PostsController : Controller
    {
        public ActionResult Index(IndexVM model, int? SubRedditId)
        {
            if(SubRedditId!=null)
            {
                model.SubRedditId = (int)SubRedditId;
            }
            PostsRepository repo = new PostsRepository();
            model.Posts = repo.GetAll(m => m.SubRedditId == model.SubRedditId);

            UsersRepository usersRepo = new UsersRepository();
            model.User = usersRepo.GetById(model.UserId);

            SubRedditsRepository subRedditsRepo = new SubRedditsRepository();
            model.SubReddit = subRedditsRepo.GetById(model.SubRedditId);

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            EditVM model = new EditVM();

            SubRedditsRepository subRedditsRepo = new SubRedditsRepository();
            List<SubReddit> subscribedSubReddits = subRedditsRepo.GetMySubscribes(AuthenticationManager.LoggedUser.Id).ToList();

            /*foreach (SubReddit subReddit in subscribedSubReddits)
            {
                model.SubRedditsList.Add(
                    new SelectListItem()
                    {
                        Value = subReddit.Id.ToString(),
                        Text = subReddit.Name,
                        Selected = subReddit.Id == model.SelectedSubReddit
                    });
            }*/

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            PostsRepository repo = new PostsRepository();
            Post item = repo.GetById(id);

            EditVM model = new EditVM(item);

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

            //model.SubRedditId = model.SelectedSubReddit;

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
            if(model.SubRedditId == 0)
                model.SubRedditId = model.SelectedSubReddit;
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

        public ActionResult Approve(int id)
        {
            PostsRepository repo = new PostsRepository();
            Post post = repo.GetById(id);

            repo.ApprovePost(id, AuthenticationManager.LoggedUser.Id);

            return RedirectToAction("Index", "Posts", new { SubRedditId = post.SubRedditId });
        }
    }
}