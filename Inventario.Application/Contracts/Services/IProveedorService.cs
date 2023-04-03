using Inventario.Domain.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Contracts.Services
{
    public interface IProveedorService
    {
        Proveedor Get(int id);

        IEnumerable<Proveedor> List(Expression<Func<Proveedor, bool>> predicate = null);

        void Insert(Proveedor proveedor);
        void Update(Proveedor proveedor);
        void Delete(int id);
        void Delete(Proveedor proveedor);
    }
}
