using DataAccess.Entities;
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
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginVM());
        }

        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            if (this.ModelState.IsValid)
            {
                AuthenticationManager.Authenticate(model.Username, model.Password);

                if (AuthenticationManager.LoggedUser == null)
                    ModelState.AddModelError("authenticationFailed", "Wrong username or password!");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            AuthenticationManager.Logout();

            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View(new RegisterVM());
        }


        [HttpPost]
        public ActionResult Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UsersRepository repo = new UsersRepository();

            if (repo.GetAll(null).FirstOrDefault(m => m.Username == model.Username) != null)
            {
                ModelState.AddModelError("RegistrationFailed", "This username already exists !");
                return View(model);
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
            AuthenticationManager.Authenticate(item.Username, item.Password);

            return RedirectToAction("Index", "Home");
            
        }

        public ActionResult GetAllSubreddits(SubRedditsVM model,int? userId)
        {
            SubRedditsRepository repo = new SubRedditsRepository();
            if (userId == null)
                model.SubRedditsList = repo.GetAll(null);
            else
                model.SubRedditsList = repo.GetAll(null).Where(x => x.SubscribedUsers.Any(b => b.Id == userId)).ToList();
            return View(model);
        }

        public ActionResult GetPostsForIndex(PostIndexVM model)
        {
            PostsRepository repo = new PostsRepository();
            SubRedditsRepository subRedditsRepository = new SubRedditsRepository();

            model.Posts = repo.GetAll(null).OrderByDescending(a => a.Rating).ToList();
            if (AuthenticationManager.LoggedUser != null)
            {
                model.SubReddits = subRedditsRepository.GetAll(null)
                .Where(x => x.SubscribedUsers.Any(b => b.Id == AuthenticationManager.LoggedUser.Id)).ToList();
            }

            return PartialView("~/Views/Partials/_IndexAllPosts.cshtml", model);
        }
    }
}