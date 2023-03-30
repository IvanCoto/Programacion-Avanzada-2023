using Inventario.Application.Contracts.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Persistence.DbContexts
{
    public class UnitOfWork<T> : IUnitOfWork<T>
        where T : DbContext
    {
        public UnitOfWork(T context)
        {
            _context = context;
            _repositories = new Dictionary<string, object>();
        }

        readonly T _context;

        IDictionary<string, object> _repositories;

        public T Context
        {
            get { return _context; }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var repository = _context.GetService<IRepository<TEntity>>();
            if (repository != null)
            {
                return repository;
            }

            var type = typeof(TEntity);
            var typeName = type.Name;

            if (_repositories.ContainsKey(typeName))
            {
                return (IRepository<TEntity>)_repositories[typeName];
            }

            var instance = new Repository<TEntity>(_context);
            _repositories.Add(typeName, instance);
            return instance;
        }
    }
}
