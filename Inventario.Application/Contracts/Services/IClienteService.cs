using Inventario.Domain.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Contracts.Services
{
    public interface IClienteService
    {
        Cliente Get(int id);

        IEnumerable<Cliente> List(Expression<Func<Cliente, bool>> predicate = null);
        
        void Insert(Cliente cliente);
        void Update(Cliente cliente);
        void Delete(int id);
        void Delete(Cliente cliente);
    }
}
