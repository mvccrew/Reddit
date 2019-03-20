using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UnitOfWork
    {
        public DbContext Context { get; private set; }

        private DbContextTransaction Transaction = null;

        public UnitOfWork()
        {
            Context = new RedditDb();
        }

        public void Begin()
        {
            if (Transaction != null)
            {
                Transaction = Context.Database.BeginTransaction();
            }
        }

        public void Commit()
        {
            if (Transaction != null)
            {
                Transaction.Commit();
            }
        }

        public void RollBack()
        {
            if (Transaction != null)
            {
                Transaction.Rollback();
                Transaction = null;
            }
        }
    }
}
