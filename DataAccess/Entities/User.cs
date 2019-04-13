using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int Karma { get; set; }

        public bool IsAdmin { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<SubReddit> SubReddits { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<SubReddit> SubscribedToSubReddits { get; set; }

        public virtual ICollection<SubReddit> AdminToSubReddits { get; set; }

        public virtual ICollection<User> Friends { get; set; }

        public virtual ICollection<SubReddit> BannedInSubReddits { get; set; }

        public virtual ICollection<SubReddit> MutedInSubReddits { get; set; }

    }
}
