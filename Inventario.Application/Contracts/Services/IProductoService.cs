using Inventario.Domain.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Contracts.Services
{
    public interface IProductoService
    {
        Producto Get(int id);
        IEnumerable<Producto> List(Expression<Func<Producto, bool>> predicate = null);
        void Insert(Producto producto);
        void Update(Producto producto);
        void Delete(int id);
        void Delete(Producto producto);
    }
}
