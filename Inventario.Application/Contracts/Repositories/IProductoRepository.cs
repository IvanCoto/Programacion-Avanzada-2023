using Inventario.Application.Contracts.DbContexts;
using Inventario.Domain.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Contracts.Repositories
{
    public interface IProductoRepository : IRepository<Producto>
    {

    }
}
