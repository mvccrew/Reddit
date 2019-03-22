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

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AuthenticationManager.LoggedUser == null ||
                (AdminArea && AuthenticationManager.LoggedUser.IsAdmin == false))
            {
                filterContext.Result = new RedirectResult("/Home/Login");
            }
        }
    }
}