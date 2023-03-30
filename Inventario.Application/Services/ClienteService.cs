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
    public class ClienteService : IClienteService
    {
        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }
        
        readonly IClienteRepository _repository;

        public Cliente Get(int id)
        {
            return _repository.Get(s => s.Id == id);
        }

        public IEnumerable<Cliente> List(System.Linq.Expressions.Expression<Func<Cliente, bool>> predicate = null)
        {
            return _repository.GetAll(predicate);
        }

        public void Insert(Cliente cliente)
        {
            _repository.Insert(cliente);
            _repository.Save();
        }

        public void Update(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException((nameof(cliente)));
            }
            _repository.Update(cliente);
            _repository.Save();
        }

        public void Delete(int id)
        {
            Delete(Get(id));
        }

        public void Delete(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException((nameof(cliente)));
            }
            _repository.Delete(cliente);
            _repository.Save();
        }

    }
}
