using Inventario.Application.Contracts.DbContexts;
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
    public class FacturaServices : IFacturaService
    {
        public FacturaServices(IFacturaRepository repository) 
        {
            _repository = repository;
        }

        readonly IFacturaRepository _repository;

        public Factura Get(int id)
        {
            return _repository.Get(s => s.Id == id);
        }

        public IEnumerable<Factura> List(Expression<Func<Factura, bool>> predicate = null)
        {
            return _repository.GetAll(predicate);
        }

        public void Insert(Factura factura)
        {
            _repository.Insert(factura);
            _repository.Save();
        }

        public void Update(Factura factura)
        {
            if (factura == null)
            {
                throw new ArgumentNullException((nameof(factura)));
            }
            _repository.Update(factura);
            _repository.Save();
        }

        public void Delete(int id)
        {
            Delete(Get(id));
        }

        public void Delete(Factura factura)
        {
            if (factura == null)
            {
                throw new ArgumentNullException((nameof(factura)));
            }
            _repository.Delete(factura);
            _repository.Save();
        }
    }
}
