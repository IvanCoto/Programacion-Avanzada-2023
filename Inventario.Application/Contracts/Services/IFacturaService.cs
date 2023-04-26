using Inventario.Domain.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Contracts.Services
{
    public interface IFacturaService
    {
        Factura Get(int id);

        IEnumerable<Factura> List(Expression<Func<Factura, bool>> predicate = null);

        void Insert(Factura factura);
        void Update(Factura factura);
        void Delete(int id);
        void Delete(Factura factura);
    }
}
