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
            
            return RedirectToAction("Index");
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

            User item = new User();
            item.Email = model.Email;
            item.Username = model.Username;
            item.Password = model.Password;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;
            repo.Insert(item);

            return RedirectToAction("Index", "Home");


        }
    }
}