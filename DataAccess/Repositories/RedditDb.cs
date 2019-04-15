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

        public DbSet<Vote> Votes { get; set; }

        public RedditDb() : base("name=RedditDbConnection")
        {
            Database.SetInitializer<RedditDb>(new RedditDbInitializer());

            this.Users = this.Set<User>();
            this.SubReddits = this.Set<SubReddit>();
            this.Comments = this.Set<Comment>();
            this.Posts = this.Set<Post>();
            this.Votes = this.Set<Vote>();
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
                    us.ToTable("UsersSubscribedToSubReddits");
                }
                );

            modelBuilder.Entity<User>()
                .HasMany<SubReddit>(u => u.AdminToSubReddits)
                .WithMany(u => u.Admins)
                .Map(us =>
                {
                    us.MapLeftKey("UserId");
                    us.MapRightKey("SubRedditId");
                    us.ToTable("UsersAdminsToSubReddits");
                }
                );

            modelBuilder.Entity<User>()
                .HasMany<User>(u => u.Friends)
                .WithMany()
                .Map(us =>
                {
                    us.MapLeftKey("UserId");
                    us.MapRightKey("FriendUserId");
                    us.ToTable("UsersToUsersFriends");
                }
                );

            modelBuilder.Entity<User>()
                .HasMany<SubReddit>(u => u.BannedInSubReddits)
                .WithMany(sr => sr.BannedUsers)
                .Map(us =>
                {
                    us.MapLeftKey("UserId");
                    us.MapRightKey("SubRedditId");
                    us.ToTable("BannedUsers");
                }
                );

            modelBuilder.Entity<User>()
                .HasMany<SubReddit>(u => u.MutedInSubReddits)
                .WithMany(sr => sr.MutedUsers)
                .Map(us =>
                {
                    us.MapLeftKey("UserId");
                    us.MapRightKey("SubRedditId");
                    us.ToTable("MutedUsers");
                }
                );

            modelBuilder.Entity<Post>()
                .HasRequired(u => u.User)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(u => u.CreationDate)
                .HasColumnType("datetime2");

            modelBuilder.Entity<SubReddit>()
                .HasRequired(u => u.User)
                .WithMany()
                .WillCascadeOnDelete(false);
                    }
    }
}
