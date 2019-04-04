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
            if (AuthenticationManager.LoggedUser == null)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
            }
            else if ((AdminArea && AuthenticationManager.LoggedUser.IsAdmin == false) ||
                    (AuthenticationManager.LoggedUser.Karma < RequiredKarma))
            {
                filterContext.Result = new RedirectResult("/Home/Index");
            }
        }
    }
}