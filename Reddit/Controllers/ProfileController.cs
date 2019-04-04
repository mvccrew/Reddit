using DataAccess.Entities;
using DataAccess.Repositories;
using Reddit.Filters;
using Reddit.ViewModels.Profile;
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
    public class ProfileController : Controller
    {
        
        [HttpGet]
        public ActionResult Index(IndexVM model)
        {
            UsersRepository repo = new UsersRepository();
            PostsRepository postsRepo = new PostsRepository();
            CommentsRepository commentsRepo = new CommentsRepository();
            
            model.User = repo.GetById(model.UserId);
            model.Posts = postsRepo.GetAll(p => p.UserId == model.UserId);
            model.Comments = commentsRepo.GetAll(c => c.UserId == model.UserId);

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            UsersRepository repo = new UsersRepository();

            User item = repo.GetById(id);

            EditVM model = new EditVM();
            model.PopulateModel(item);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(String.Empty, "Something went wrong :(");
                return View(model);
            }

            UsersRepository repo = new UsersRepository();
            User item = new User();

            model.PopulateEntity(item);

            repo.Save(item);

            return RedirectToAction("Index", "Profile", new { UserId = item.Id });
        }

    }
}