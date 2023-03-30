using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Contracts.DbContexts
{
    public interface IRepository<T>
    {
        T Get(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes);

        IEnumerable<T> GetAll
            (Expression<Func<T, bool>> predicate = null,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             params Expression<Func<T, object>>[] includes);

        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

        void Save();
    }
}
