using Inventario.Application.Contracts.Repositories;
using Inventario.Domain.EntityModels;
using Inventario.Persistence.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Persistence.Repositories
{
    public class ProductoRepository : Repository<Producto>, IProductoRepository
    {
        public ProductoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
