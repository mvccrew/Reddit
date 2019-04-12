using Reddit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reddit.Filters
{
    public class AuthenticationFilter : ActionFilterAttribute
    {
        public bool AdminArea { get; set; }

        public int RequiredKarma { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AuthManager.LoggedUser == null)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
            }
            else if ((AdminArea && AuthManager.LoggedUser.IsAdmin == false) ||
                    (AuthManager.LoggedUser.Karma < RequiredKarma))
            {
                filterContext.Result = new RedirectResult("/Home/Index");
            }
        }
    }
}