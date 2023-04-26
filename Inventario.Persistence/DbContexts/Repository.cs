using Inventario.Application.Contracts.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Persistence.DbContexts
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        public Repository(DbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        readonly DbContext _context;
        readonly DbSet<T> _set;


        public T Get(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
        {


            IQueryable<T> query = _set;

            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query.FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetAll
            (Expression<Func<T, bool>> predicate = null,
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _set;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }


            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }




        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }


            _set.Add(entity);
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _set.Update(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _set.Remove(entity);
        }
        // Principio DRY: Don't Repeat Yourself, No te repitas

        public void Save()
        {
            _context.SaveChanges();
        }


    }
}
