using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }

        public int Rating { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

        public int? ParentCommentId { get; set; }

        public virtual ICollection<Comment> SubComments { get; set; }
    }
}
