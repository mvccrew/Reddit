using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class SubRedditsRepository : BaseRepository<SubReddit>
    {

        public void Subscribe(int subRedditId, int userId)
        {
            RedditDb context = new RedditDb();

            User user = context.Users.Find(userId);

            SubReddit subReddit = context.SubReddits.Find(subRedditId);

            user.SubscribedToSubReddits.Add(subReddit);
            subReddit.SubscribedUsers.Add(user);
            context.SaveChanges();
        }
        public void UnSubscribe(int subRedditId, int userId)
        {
            RedditDb context = new RedditDb();

            User user = context.Users.Find(userId);

            SubReddit subReddit = context.SubReddits.Find(subRedditId);

            user.SubscribedToSubReddits.Remove(subReddit);
            subReddit.SubscribedUsers.Remove(user);
            context.SaveChanges();
        }

        public void AddAdminToSubReddit(int subRedditId, int userId)
        {
            RedditDb context = new RedditDb();

            User user = context.Users.Find(userId);

            SubReddit subReddit = context.SubReddits.Find(subRedditId);

            user.AdminToSubReddits.Add(subReddit);
            subReddit.Admins.Add(user);
            context.SaveChanges();
        }

        public IQueryable<SubReddit> GetMySubscribes(int userId)
        {
            RedditDb context = new RedditDb();

            IQueryable<SubReddit> result = context.SubReddits.Where(sr => sr.SubscribedUsers.Any(u => u.Id == userId));

            return result;
        }
    }
}
