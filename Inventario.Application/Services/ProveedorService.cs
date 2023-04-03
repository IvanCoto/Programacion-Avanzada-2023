using Inventario.Application.Contracts.Repositories;
using Inventario.Application.Contracts.Services;
using Inventario.Domain.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Services
{
    public class ProveedorService : IProveedorService
    {
        public ProveedorService(IProveedorRepository repository)
        {
            _repository = repository;
        }
        
        readonly IProveedorRepository _repository;


        public Proveedor Get(int id)
        {
            return _repository.Get(s => s.Id == id);
        }

        public IEnumerable<Proveedor> List(Expression<Func<Proveedor, bool>> predicate = null)
        {
            return _repository.GetAll(predicate);
        }

        public void Insert(Proveedor proveedor)
        {
            _repository.Insert(proveedor);
            _repository.Save();
        }

        

        public void Update(Proveedor proveedor)
        {
            if (proveedor == null)
            {
                throw new ArgumentNullException((nameof(proveedor)));
            }
            _repository.Update(proveedor);
            _repository.Save();
        }

        public void Delete(int id)
        {
            Delete(Get(id));
        }

        public void Delete(Proveedor proveedor)
        {
            if (proveedor == null)
            {
                throw new ArgumentNullException((nameof(proveedor)));
            }
            _repository.Delete(proveedor);
            _repository.Save();
        }
    }
}
