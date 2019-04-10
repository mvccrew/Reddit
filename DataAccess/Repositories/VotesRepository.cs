using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    // не работи коректно, когато воутна коментар с ид=2, и в същото време има пост с ид=2!!!!
    public class VotesRepository : BaseRepository<Vote>
    {
        public void Vote(int userId, int contentId, int value, string type)
        {
            PostsRepository postsRepo = new PostsRepository();
            CommentsRepository commentsRepo = new CommentsRepository();
            VotesRepository votesRepo = new VotesRepository();
            UsersRepository usersRepo = new UsersRepository();

            RedditDb context = new RedditDb();

            //ако не съществува такъв запис в таблица Votes, се създава, а рейтингът на поста се увеличава със стойността на вота
            if (context.Votes.Where(a => a.UserId == userId && a.ContentId == contentId && a.Type.ToString() == type).Count() == 0)
            {
                if (type == "Post")
                {
                    Post post = postsRepo.GetById(contentId);
                    post.Rating += value;
                    postsRepo.Save(post);

                    usersRepo.ChangeKarma(post.UserId, value);

                    votesRepo.Insert(new Vote() { UserId = userId, ContentId = contentId, CreationDate = DateTime.Now, Value = value,Type= Entities.Type.Post });
                    context.SaveChanges();
                }
                else if (type == "Comment")
                {
                    Comment comment = commentsRepo.GetById(contentId);
                    comment.Rating += value;
                    commentsRepo.Save(comment);

                    usersRepo.ChangeKarma(comment.UserId, value);

                    votesRepo.Insert(new Vote() { UserId = userId, ContentId = contentId, CreationDate = DateTime.Now, Value = value, Type=Entities.Type.Comment });
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(message: "Something went wrong :(");
                }
            }

            //ако обаче съществува такъв, проверява дали стойността на вота е същата. Ако е - изтрива записа, ако не е - го променя
            else 
            {
                if (context.Votes.Any(v => v.UserId == userId && v.ContentId == contentId && v.Value == value && v.Type.ToString() == type))
                {
                    if (type == "Post")
                    {
                        Post post = postsRepo.GetById(contentId);
                        post.Rating -= value;
                        postsRepo.Save(post);

                        usersRepo.ChangeKarma(post.UserId, -1*value);

                        Vote vote = votesRepo.GetAll(v => v.UserId == userId && v.ContentId == contentId && v.Value == value && v.Type.ToString() == type).FirstOrDefault();
                        votesRepo.Delete(vote);
                        context.SaveChanges();
                    }
                    else if (type == "Comment")
                    {
                        Comment comment = commentsRepo.GetById(contentId);
                        comment.Rating -= value;
                        commentsRepo.Save(comment);

                        usersRepo.ChangeKarma(comment.UserId, -1*value);

                        Vote vote = votesRepo.GetAll(v => v.UserId == userId && v.ContentId == contentId && v.Value == value && v.Type.ToString() == type).FirstOrDefault();
                        votesRepo.Delete(vote);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception(message: "Something went wrong :(");
                    }
                }

                else
                {
                    if (type == "Post")
                    {
                        Post post = postsRepo.GetById(contentId);
                        post.Rating += 2*value;
                        postsRepo.Save(post);

                        usersRepo.ChangeKarma(post.UserId, 2*value);

                        Vote vote = votesRepo.GetAll(v => v.UserId == userId && v.ContentId == contentId && v.Value != value && v.Type.ToString() == type).FirstOrDefault();
                        vote.Value += 2*value;
                        votesRepo.Update(vote);
                        context.SaveChanges();
                    }
                    else if (type == "Comment")
                    {
                        Comment comment = commentsRepo.GetById(contentId);
                        comment.Rating += 2 * value;
                        commentsRepo.Save(comment);

                        usersRepo.ChangeKarma(comment.UserId, 2 * value);

                        Vote vote = votesRepo.GetAll(v => v.UserId == userId && v.ContentId == contentId && v.Value != value && v.Type.ToString() == type).FirstOrDefault();
                        vote.Value += 2 * value;
                        votesRepo.Update(vote);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
