using Inventario.Domain.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Contracts.Services
{
    public interface IVentaService
    {
        Venta Get(int id);

        IEnumerable<Venta> List(Expression<Func<Venta, bool>> predicate = null);
        
        void Insert(Venta Venta);
        void Update(Venta Venta);
    }
}
