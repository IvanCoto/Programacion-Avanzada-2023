using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Contracts.DbContexts
{
    public interface IUnitOfWork<T>
    where T : class
    {
        T Context { get; }

        void Save();

        IRepository<TEntity> Repository<TEntity>()
            where TEntity : class;
    }
}
