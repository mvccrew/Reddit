using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class RedditDb : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<SubReddit> SubReddits { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Post> Posts { get; set; }

        public RedditDb() : base("name=RedditDbConnection")
        {
            this.Users = this.Set<User>();
            this.SubReddits = this.Set<SubReddit>();
            this.Comments = this.Set<Comment>();
            this.Posts = this.Set<Post>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany<SubReddit>(u => u.SubscribedToSubReddits)
                .WithMany(u => u.SubscribedUsers)
                .Map(us =>
                {
                    us.MapLeftKey("UserId");
                    us.MapRightKey("SubRedditId");
                    us.MapRightKey("UsersSubscribedToSubReddits");
                }
                );

            modelBuilder.Entity<User>()
                .HasMany<SubReddit>(u => u.AdminToSubReddits)
                .WithMany(u => u.Admins)
                .Map(us =>
                {
                    us.MapLeftKey("UserId");
                    us.MapRightKey("SubRedditId");
                    us.MapRightKey("UsersAdminsToSubReddits");
                }
                );

            modelBuilder.Entity<User>()
                .HasRequired(u => u.Posts)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasRequired(u => u.SubReddits)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}
