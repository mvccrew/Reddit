using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class VotesRepository : BaseRepository<Vote>
    {
        public void Vote(int userId, int postId, int value)
        {
            PostsRepository postsRepo = new PostsRepository();
            VotesRepository votesRepo = new VotesRepository();
            UsersRepository usersRepo = new UsersRepository();

            RedditDb context = new RedditDb();

            //ако не съществува такъв запис в таблица Votes, се създава, а рейтингът на поста се увеличава със стойността на вота
            if (!context.Votes.Any(v => v.UserId == userId && v.PostId == postId))
            {
                Post post = postsRepo.GetById(postId);
                post.Rating += value;
                postsRepo.Save(post);

                usersRepo.ChangeKarma(post.UserId, value);

                votesRepo.Insert(new Vote() { UserId = userId, PostId = postId, CreationDate = DateTime.Now, Value = value });
                context.SaveChanges();
            }

            //ако обаче съществува такъв, проверява дали стойността на вота е същата. Ако е - изтрива записа, ако не е - го променя
            else if (context.Votes.Any(v => v.UserId == userId && v.PostId == postId))
            {
                if (context.Votes.Any(v => v.UserId == userId && v.PostId == postId && v.Value == value))
                {
                    Post post = postsRepo.GetById(postId);
                    post.Rating -= value;
                    postsRepo.Save(post);

                    usersRepo.ChangeKarma(post.UserId, value);

                    Vote vote = votesRepo.GetAll(v => v.UserId == userId && v.PostId == postId && v.Value == value).FirstOrDefault();
                    votesRepo.Delete(vote);
                    context.SaveChanges();
                }

                else
                {
                    Post post = postsRepo.GetById(postId);
                    post.Rating += 2*value;
                    postsRepo.Save(post);

                    usersRepo.ChangeKarma(post.UserId, 2*value);

                    Vote vote = votesRepo.GetAll(v => v.UserId == userId && v.PostId == postId && v.Value != value).FirstOrDefault();
                    vote.Value += 2*value;
                    votesRepo.Update(vote);
                    context.SaveChanges();
                }
            }
        }
    }
}
