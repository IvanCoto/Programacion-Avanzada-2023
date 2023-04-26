using Inventario.Domain.EntityModels;
using Inventario.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Contracts.Inventario
{
    public interface IClienteClient
    {

        //Get
        Task<List<Cliente>> GetListaCliente();

        //Get{id}
        Task<ClienteViewModel> ObtenerCliente(int id);

        //Post
        Task<bool> GuardarCliente(Cliente cliente);

        //Put
        Task<bool> EditarCliente(Cliente cliente);

        //Delete
        Task<bool> EliminarCliente(int id);

    }
}
