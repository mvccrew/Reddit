using DataAccess.Entities;
using DataAccess.Repositories;
using Reddit.Filters;
using Reddit.ViewModels.Votes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reddit.Controllers
{
    [AuthenticationFilter]
    public class VotesController : Controller
    {
        public ActionResult Vote(VoteVM model)
        {
            VotesRepository votesRepo = new VotesRepository();

            votesRepo.Vote(Models.AuthenticationManager.LoggedUser.Id, model.ContentId, model.Value, model.Type);

            //тва някой да го опрай майка му стара да връща същата страница че полудях
            return RedirectToAction("Index", model.Type.ToString() + "s", new { model.ContentId });
        }

    }
}