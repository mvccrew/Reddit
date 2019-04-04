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

        public int ContentId { get; set; }

        public string Type { get; set; }

        public VoteVM()
        {

        }

        public VoteVM(int contentId, string type)
        {
            this.ContentId = contentId;
            this.Type = type;
        }

    }
}