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
    public class ProveedorRepository : Repository<Proveedor>, IProveedorRepository
    {
        public ProveedorRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
