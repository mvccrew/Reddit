﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public enum PostType
    {
        Undefined = 0,
        TextPost = 1,
        ImagePost = 2,
        VideoPost = 3
    }
    public class Post : BaseEntity
    {
        public string Title { get; set; }

        public int Rating { get; set; }

        public string Content { get; set; }

        public PostType PostType { get; set; }
        
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Posts")]
        public virtual User User { get; set; }

        public int SubRedditId { get; set; }

        public bool IsApproved { get; set; }

        public virtual SubReddit SubReddit { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
