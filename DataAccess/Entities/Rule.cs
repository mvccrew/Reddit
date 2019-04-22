using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Rule : BaseEntity
    {
        public string Text { get; set; }

        public int SubRedditId { get; set; }
    }
}
