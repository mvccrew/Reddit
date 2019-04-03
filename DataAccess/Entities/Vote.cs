using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Vote : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        public int Value { get; set; }
    }
}
