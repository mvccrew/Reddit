using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reddit.ViewModels.Votes
{
    public class VoteVM
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public VoteVM()
        {

        }

        public VoteVM(int postId)
        {
            this.PostId = postId;
        }

    }
}