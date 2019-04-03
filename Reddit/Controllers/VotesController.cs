using DataAccess.Entities;
using DataAccess.Repositories;
using Reddit.ViewModels.Votes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reddit.Controllers
{
    public class VotesController : Controller
    {
        public ActionResult Vote(VoteVM model)
        {
            VotesRepository votesRepo = new VotesRepository();

            votesRepo.Vote(Models.AuthenticationManager.LoggedUser.Id, model.PostId, model.Value);

            return RedirectToAction("Index", "Home");
        }

    }
}