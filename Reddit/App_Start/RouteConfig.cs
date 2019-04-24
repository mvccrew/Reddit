using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Reddit
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            SubRedditsRepository repo = new SubRedditsRepository();
            List<SubReddit> items = repo.GetAll(null);
            foreach (SubReddit sr in items)
            {
                routes.MapRoute(
                    name: "SubReddit" + sr.Id,
                    url: sr.Name,
                    defaults: new { controller = "Posts", action = "Index", SubRedditId = sr.Id });
            }

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}
