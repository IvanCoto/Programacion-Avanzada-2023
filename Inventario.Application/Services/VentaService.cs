using Inventario.Application.Contracts.DbContexts;
using Inventario.Application.Contracts.Repositories;
using Inventario.Application.Contracts.Services;
using Inventario.Domain.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Services
{
    public class VentaService : IVentaService
    {
        public VentaService(IVentaRepository repository)
        {
            _repository = repository;
        }
        
        readonly IVentaRepository _repository;

        public Venta Get(int id)
        {
            return _repository.Get(s => s.Id == id);
        }

        public IEnumerable<Venta> List(System.Linq.Expressions.Expression<Func<Venta, bool>> predicate = null)
        {
            return _repository.GetAll(predicate);
        }

        public void Insert(Venta Venta)
        {
            _repository.Insert(Venta);
            _repository.Save();
        }

        
        

    }
}
