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
            PostsRepository postsRepository = new PostsRepository();
            UsersRepository usersRepository = new UsersRepository();

            List<SubReddit> items = repo.GetAll(null);
            List<Post> posts = postsRepository.GetAll(null);
            List<User> users = usersRepository.GetAll(null);
            foreach (SubReddit sr in items)
            {
                routes.MapRoute(
                    name: "SubReddit" + sr.Id,
                    url: "r/" + sr.Name,
                    defaults: new { controller = "Posts", action = "Index", SubRedditId = sr.Id });
                foreach (Post item in posts.Where(a => a.SubRedditId==sr.Id))
                {
                    routes.MapRoute(
                    name: "Post" + item.Id,
                    url: "r/" + sr.Name +"/" + "comments"+ '/' + getTitleForURL(item.Title.ToLower()),
                    defaults: new { controller = "Comments", action = "Index", PostId = item.Id });
                }
            }
            foreach (User item in users)
            {
                routes.MapRoute(
                name: "Profile" + item.Id,
                url: "user/" + item.Username,
                defaults: new { controller = "Profile", action = "Index", UserId = item.Id }
                 );
            }

            /*foreach(var item in Reddit.Models.AuthManager.LoggedUser.SubscribedToSubReddits)
            {
                routes.MapRoute(
                name: "PostsCreate",
                url: "r/" + item.Name + "/submit"  ,
                defaults: new { controller = "Posts", action = "Create", id = UrlParameter.Optional }
                 );
            }

            string allOrHome = "";
            if (Reddit.Models.AuthManager.LoggedUser != null) allOrHome = "r/home";
            else allOrHome = "r/all";
            routes.MapRoute(
               name: "allHome",
               url: allOrHome,
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );*/
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional}
            );

        }
        public static string getTitleForURL(string title)
        {
            var charsToRemove = new string[] { "@", ",", ".", ";", "'","-" };
            foreach (var c in charsToRemove)
            {
                title = title.Replace(c, string.Empty);
            }
            title = title.Replace(" ", "_");
            return title;
        }
    }
}
