using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class SubReddit : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Theme { get; set; }

        public string Rules { get; set; }
        
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("SubReddits")]
        public virtual User User { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<User> Admins { get; set; }

        public virtual ICollection<User> SubscribedUsers { get; set; }

    }
}
