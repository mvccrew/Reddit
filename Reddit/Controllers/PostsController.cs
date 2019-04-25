using DataAccess.Entities;
using DataAccess.Repositories;
using Reddit.Filters;
using Reddit.Models;
using Reddit.ViewModels.Posts;
using Reddit.ViewModels.Share;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Reddit.Controllers
{   
    [AuthenticationFilter(RequiredKarma = int.MinValue)]
    public class PostsController : Controller
    {
        [BanFilter]
        public ActionResult Index(IndexVM model, int? SubRedditId)
        {
            if(SubRedditId!=null)
            {
                model.SubRedditId = (int)SubRedditId;
            }
            PostsRepository repo = new PostsRepository();
            if(AuthManager.LoggedUser.AdminToSubReddits.Any(m => m.Admins.Any(a => a.Id==AuthManager.LoggedUser.Id)))
            {
                model.Posts = repo.GetAll(m => m.SubRedditId == model.SubRedditId).OrderByDescending(a => a.Rating).ToList();
            }
            else
            {
                model.Posts = repo.GetAll(m => m.SubRedditId == model.SubRedditId && m.IsApproved == true).OrderByDescending(a => a.Rating).ToList();
            }

            UsersRepository usersRepo = new UsersRepository();
            model.User = usersRepo.GetById(model.UserId);

            SubRedditsRepository subRedditsRepo = new SubRedditsRepository();
            model.SubReddit = subRedditsRepo.GetById(model.SubRedditId);

            return View(model);
        }

        [BanFilter]
        [MuteFilter]
        [HttpGet]
        public ActionResult Create()
        {
            EditVM model = new EditVM();

            model.SubRedditsList = new List<SelectListItem>();
            model.UserId = AuthManager.LoggedUser.Id;

            SubRedditsRepository subRedditsRepo = new SubRedditsRepository();
            List<SubReddit> subscribedSubReddits = subRedditsRepo.GetMySubscribes(AuthManager.LoggedUser.Id).ToList();

            foreach (SubReddit subReddit in subscribedSubReddits)
            {
                model.SubRedditsList.Add(
                    new SelectListItem()
                    {
                        Value = subReddit.Id.ToString(),
                        Text = "r/" + subReddit.Name,
                        Selected = subReddit.Id == model.SelectedSubReddit,
                        Disabled = (((AuthManager.LoggedUser.BannedInSubReddits.Any(sr => sr.Id == subReddit.Id
                                       && sr.BannedUsers.Any(u => u.Id == AuthManager.LoggedUser.Id))) ||
                                       (AuthManager.LoggedUser.MutedInSubReddits.Any(sr => sr.Id == subReddit.Id
                                        && sr.MutedUsers.Any(u => u.Id == AuthManager.LoggedUser.Id)))))
                                    ? true : false
                    });
            }

            return View(model);
        }

        [BanFilter]
        [MuteFilter]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            PostsRepository repo = new PostsRepository();
            Post item = repo.GetById(id);

            EditVM model = new EditVM(item);

            model.SubRedditsList = new List<SelectListItem>();

            SubRedditsRepository subRedditsRepo = new SubRedditsRepository();
            List<SubReddit> subscribedSubReddits = subRedditsRepo.GetMySubscribes(AuthManager.LoggedUser.Id).ToList();

            foreach (SubReddit subReddit in subscribedSubReddits)
            {
                if (!(AuthManager.LoggedUser.BannedInSubReddits.Any(sr => sr.BannedUsers.Any(u => u.Id == AuthManager.LoggedUser.Id)) ||
                    AuthManager.LoggedUser.MutedInSubReddits.Any(sr => sr.MutedUsers.Any(u => u.Id == AuthManager.LoggedUser.Id))))
                    model.SubRedditsList.Add(
                    new SelectListItem()
                    {
                        Value = subReddit.Id.ToString(),
                        Text = subReddit.Name,
                        Selected = subReddit.Id == model.SelectedSubReddit
                    });
            }

            //model.SubRedditId = model.SelectedSubReddit;

            return PartialView("~/Views/Partials/Edits/_EditPost.cshtml", model);
        }

        [BanFilter]
        [MuteFilter]
        [HttpPost]
        public ActionResult Edit(EditVM model, FormCollection form)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }
            SubRedditsRepository subRedditsRepo = new SubRedditsRepository();
            PostsRepository postsRepo = new PostsRepository();

            Post item = new Post();
            if (model.SubRedditId == 0)
                model.SubRedditId = model.SelectedSubReddit;

            if ((Request.Files["image"] != null) && (!String.IsNullOrEmpty(Request.Files["image"].FileName)) && (Request.Files["image"].ContentLength > 0))
            {
                if (FileFormats.Formats.ImageFormats.Contains(Request.Files["image"].FileName.Split('.').Last().ToLower()) ||
                    FileFormats.Formats.VideoFormats.Contains(Request.Files["image"].FileName.Split('.').Last().ToLower()))
                {
                    string file_path = Server.MapPath("~/Content/img/post_images/");
                    Request.Files["image"].SaveAs(file_path + Request.Files["image"].FileName);
                    model.Content = file_path + Request.Files["image"].FileName;
                    model.PopulateEntity(item);
                    postsRepo.Save(item);
                    return RedirectToAction("Index", "Posts", new { SubRedditId = model.SubRedditId });
                }
                else
                {
                    ModelState.AddModelError("notSupportedFormat", "This format is not supported :(");

                    model.SubRedditsList = new List<SelectListItem>();

                    List<SubReddit> subscribedSubReddits = subRedditsRepo.GetMySubscribes(AuthManager.LoggedUser.Id).ToList();

                    foreach (SubReddit subReddit in subscribedSubReddits)
                    {
                        if (!(AuthManager.LoggedUser.BannedInSubReddits.Any(sr => sr.BannedUsers.Any(u => u.Id == AuthManager.LoggedUser.Id)) ||
                            AuthManager.LoggedUser.MutedInSubReddits.Any(sr => sr.MutedUsers.Any(u => u.Id == AuthManager.LoggedUser.Id))))
                            model.SubRedditsList.Add(
                            new SelectListItem()
                            {
                                Value = subReddit.Id.ToString(),
                                Text = subReddit.Name,
                                Selected = subReddit.Id == model.SelectedSubReddit
                            });
                    }
                    return View("Create", model);
                }
            }

            model.PopulateEntity(item);
            postsRepo.Save(item);

            return RedirectToAction("Index", "Posts", new { SubRedditId = model.SubRedditId });

            //return Content("");
        }

        [BanFilter]
        [MuteFilter]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            PostsRepository repo = new PostsRepository();

            Post item = repo.GetById(id);

            repo.Delete(item);

            return RedirectToAction("Index", "Posts", new { SubRedditId = item.SubRedditId });
        }

        [BanFilter]
        [MuteFilter]
        public ActionResult Approve(int id)
        {
            PostsRepository repo = new PostsRepository();
            Post post = repo.GetById(id);

            repo.ApprovePost(id, AuthManager.LoggedUser.Id);

            return RedirectToAction("Index", "Posts", new { SubRedditId = post.SubRedditId });
        }
    }
}