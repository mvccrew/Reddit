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
        public ActionResult Vote(VoteVM voteModel)
        {
            VotesRepository votesRepo = new VotesRepository();

            votesRepo.Vote(Models.AuthenticationManager.LoggedUser.Id, voteModel.ContentId, voteModel.Value, voteModel.Type);

            //тва някой да го опрай майка му стара да връща същата страница че полудях
            if(voteModel.Type.ToString()=="Post")
            { 
            return RedirectToAction("Index", voteModel.Type.ToString() + "s", new { SubRedditId = voteModel.ContentId2});
            }
            else
                return RedirectToAction("Index", voteModel.Type.ToString() + "s", new { PostId = voteModel.ContentId2 });
        }

    }
}