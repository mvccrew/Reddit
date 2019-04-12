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
    [AuthenticationFilter(RequiredKarma = int.MinValue)]
    public class VotesController : Controller
    {
        public ActionResult Vote(VoteVM voteModel)
        {
            VotesRepository votesRepo = new VotesRepository();

            votesRepo.Vote(Models.AuthManager.LoggedUser.Id, voteModel.ContentId, voteModel.Value, voteModel.Type);

            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}