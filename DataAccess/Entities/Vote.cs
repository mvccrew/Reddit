using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public enum Type
    {
        Post = 1,
        Comment = 2
    }
    public class Vote : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public Type Type { get; set; }
        public int ContentId { get; set; }

        public int Value { get; set; }
    }
}
