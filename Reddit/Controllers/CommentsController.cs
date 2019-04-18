using DataAccess.Entities;
using DataAccess.Repositories;
using Reddit.Filters;
using Reddit.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reddit.Controllers
{
    public class CommentsController : Controller
    {
        // GET: Comments
        [AuthenticationFilter(RequiredKarma = int.MinValue)]
        public ActionResult Index(IndexVM model, int? PostId)
        {
            if(PostId != null)
            {
                model.PostId = (int)PostId;
            }
            CommentsRepository repo = new CommentsRepository();
            model.Items = repo.GetAll(m => m.PostId == model.PostId);

            PostsRepository postsRepo = new PostsRepository();
            model.Post = postsRepo.GetById(model.PostId);

            UsersRepository usersRepo = new UsersRepository();
            model.User = usersRepo.GetById(model.UserId);

            return View(model);
        }

        [AuthenticationFilter(RequiredKarma = int.MinValue)]
        [HttpGet]
        public ActionResult Edit(int? id, int? PostId, int? parentCommentId)
        {
            Comment item = null;

            CommentsRepository repo = new CommentsRepository();

            item = id == null ? new Comment() : repo.GetById(id.Value);

            EditVM model = item == null ? new EditVM() : new EditVM(item);

            if (model.Id <= 0)
            {
                model.PostId = PostId.Value;
            }
            return PartialView("~/Views/Partials/Edits/_EditComment.cshtml", model);
        }

        [AuthenticationFilter(RequiredKarma = int.MinValue)]
        [HttpPost]
        public ActionResult Edit(EditVM model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("~/Views/Partials/Edits/_EditComment.cshtml", model);
            }

            CommentsRepository repo = new CommentsRepository();
            Comment item = new Comment();
            model.PopulateEntity(item);

            repo.Save(item);

            return Content("");
        }
    }
}