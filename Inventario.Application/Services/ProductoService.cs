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
    public class ProductoService : IProductoService
    {

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }
        readonly IProductoRepository _repository;


        public Producto Get(int id)
        {
            return _repository.Get(s => s.Id == id , includes: i => i.Proveedor);
        }

        public IEnumerable<Producto> List(Expression<Func<Producto, bool>> predicate = null)
        {
            //return _repository.GetAll(predicate);
            return _repository.GetAll(includes: i => i.Proveedor);
        }

        public void Insert(Producto producto)
        {
            _repository.Insert(producto);
            _repository.Save();
        }



        public void Update(Producto producto)
        {
            if (producto == null)
            {
                throw new ArgumentNullException((nameof(producto)));
            }
            _repository.Update(producto);
            _repository.Save();
        }
        public void Delete(int id)
        {
            Delete(Get(id));
        }

        public void Delete(Producto producto)
        {
            if (producto == null)
            {
                throw new ArgumentNullException((nameof(producto)));
            }
            _repository.Delete(producto);
            _repository.Save();
        }

    }
}
