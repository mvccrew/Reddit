using DataAccess.Entities;
using Reddit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reddit.Filters
{
    public class BanFilter : ActionFilterAttribute
    {
        public int SubRedditId { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var url = filterContext.HttpContext.Request.Url.ToString();
            if (url.Contains('='))
            SubRedditId = Int32.Parse(url.Split('=').Last());

            if (AuthManager.LoggedUser.BannedInSubReddits.Any(sr => sr.Id == (int)SubRedditId && sr.BannedUsers.Any(u => u.Id == AuthManager.LoggedUser.Id)))
            {
                filterContext.Result = new RedirectResult("/Home/Index/#banned");
            }
        }

        /*public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var url = filterContext.HttpContext.Request.Url.ToString();
            if (url.Contains('='))
                SubRedditId = Int32.Parse(url.Split('=').Last());

            if (AuthManager.LoggedUser.BannedInSubReddits.Any(sr => sr.Id == (int)SubRedditId && sr.BannedUsers.Any(u => u.Id == AuthManager.LoggedUser.Id)))
            {
                filterContext.Result = new RedirectResult("/Home/Index/#banned");
            }
        }*/
    }
}