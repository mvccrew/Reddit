﻿using System;
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
    [AuthenticationFilter]
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

            return RedirectToAction("Index", "SubReddit");
        }

        public ActionResult Subscribe(int id)
        {
            SubRedditsRepository repo = new SubRedditsRepository();

            repo.Subscribe(id, AuthenticationManager.LoggedUser.Id);

            return RedirectToAction("Index", "Home");
        }

    }
}