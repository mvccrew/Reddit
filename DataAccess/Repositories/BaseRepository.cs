using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public abstract class BaseRepository<E>
        where E : BaseEntity, new()
    {
        protected DbContext Context { get; private set; }

        protected UnitOfWork UnitOfWork { get; set; }

        protected DbSet<E> Items { get; set; }

        public BaseRepository() : this(null)
        {

        }

        public BaseRepository(UnitOfWork aUnitOfWork)
        {
            if (aUnitOfWork == null)
            {
                Context = new RedditDb();
            }
            else
            {
                UnitOfWork = aUnitOfWork;
                Context = aUnitOfWork.Context;
            }

            Items = Context.Set<E>();
        }

        public E GetById(int id)
        {
            return Items.Find(id);
        }

        public void Insert(E item)
        {
            Items.Add(item);
            Context.SaveChanges();
        }

        public void Delete(E item)
        {
            Items.Remove(item);
            Context.SaveChanges();
        }

        public void Update(E item)
        {
            Context.Entry(item).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Save(E item)
        {
            if (item.Id <= 0)
            {
                Insert(item);
            }
            else
            {
                Update(item);
            }
        }

        public int Count(Expression<Func<E,bool>> filter)
        {
            IQueryable<E> query = Items;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }

        public List<E> GetAll(Expression<Func<E,bool>> filter, int page = 1, int itemsPerPage = int.MaxValue,
                              Func<IQueryable<E>, IOrderedQueryable<E>> orderByDescending = null)
        {
            IQueryable<E> query = Items;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderByDescending == null)
            {
                orderByDescending = i => i.OrderBy(x => x.Id);
            }

            query = orderByDescending(query);

            if (page > 0 && itemsPerPage > 0)
            {
                query = query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            }

            return query.ToList();
        }
    }
}
