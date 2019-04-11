using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PostsRepository : BaseRepository<Post>
    {
        public void ApprovePost(int postId, int userId)
        {
            RedditDb context = new RedditDb();
            
            User user = context.Users.Find(userId);
            Post post = context.Posts.Find(postId);

            if (context.Users.AsNoTracking().Where(u => u.AdminToSubReddits.Any(sr => sr.Admins.Any(us => us.Id == userId))).Count() > 0)
            {
                try
                {
                    post.IsApproved = true;
                    context.Entry(post).Property(x => x.IsApproved).IsModified = true;
                    context.SaveChanges();
                }

                catch (Exception)
                {
                }
            }
        }
    }
}
