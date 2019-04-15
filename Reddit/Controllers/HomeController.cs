﻿using DataAccess.Entities;
using DataAccess.Repositories;
using Reddit.Models;
using Reddit.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reddit.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            PostIndexVM model = new PostIndexVM();
            PostsRepository repo = new PostsRepository();
            SubRedditsRepository subRedditsRepository = new SubRedditsRepository();

            model.Posts = repo.GetAll(p => p.IsApproved == true).OrderByDescending(a => a.Rating).ToList();
            if (AuthManager.LoggedUser != null)
            {
                model.SubReddits = subRedditsRepository.GetAll(null)
                .Where(x => x.SubscribedUsers.Any(b => b.Id == AuthManager.LoggedUser.Id)).OrderByDescending(c => c.Id).ToList();
            }
            else
            {
                model.SubReddits = subRedditsRepository.GetAll(null);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return Redirect((Request.UrlReferrer == null) ? "/Home/Index#notloggedin" : Request.UrlReferrer.ToString() + "#notloggedin");
        }

        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            if (this.ModelState.IsValid)
            {
                AuthManager.Authenticate(model.Username, model.Password);

                if (AuthManager.LoggedUser == null)
                    ModelState.AddModelError("authenticationFailed", "Wrong username or password!");
            }

            if (!ModelState.IsValid)
            {
                return PartialView("~/Views/Partials/_Login.cshtml", model);
            }

            return Content("");
        }

        public ActionResult Logout()
        {
            AuthManager.Logout();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (AuthManager.LoggedUser == null)
                return View(new RegisterVM());
            else
                return Redirect("Index");
        }


        [HttpPost]
        public ActionResult Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("~/Views/Partials/_Register.cshtml", model);
            }

            UsersRepository repo = new UsersRepository();

            if (repo.GetAll(null).FirstOrDefault(m => m.Username == model.Username) != null)
            {
                ModelState.AddModelError("RegistrationFailed", "This username already exists!");
                return PartialView("~/Views/Partials/_Register.cshtml", model);
            }

            User item = new User
            {
                Username = model.Username,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                CreationDate = DateTime.Now
                
            };

            repo.Insert(item);

            // automatic login after registration
            AuthManager.Authenticate(item.Username, item.Password);

            return Content("");
            
        }

        [HttpGet]
        public ActionResult GetAllSubreddits(SubRedditsVM model, int? userId)
        {
            SubRedditsRepository repo = new SubRedditsRepository();
            if (userId == null)
                model.SubRedditsList = repo.GetAll(null);
            else
                model.SubRedditsList = repo.GetAll(x => x.SubscribedUsers.Any(b => b.Id == userId)).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Search(SearchVM model, int? postId, int? subredditId)
        {
            SubRedditsRepository subRepo = new SubRedditsRepository();
            PostsRepository postRepo = new PostsRepository();
            model.SubReddits = subRepo.GetAll(a => a.Name.Contains(model.Filter)).ToList();

            model.Posts = postRepo.GetAll(a => a.Title.ToLower().Contains(model.Filter.ToLower()) ||
            a.SubReddit.Name.ToLower().Contains(model.Filter.ToLower()) ||
            a.Comments.Any(b => b.Text.ToLower().Contains(model.Filter.ToLower())) ||
            a.Content.ToLower().Contains(model.Filter.ToLower())).OrderByDescending(b => b.Rating).ToList();
            return View(model);
        }
        /*
         * a => a.Title.Contains(model.Filter) ||
            a.SubReddit.Name.Contains(model.Filter) ||
            a.Comments.Any( b => b.Text.Contains(model.Filter))
         */
    }
}