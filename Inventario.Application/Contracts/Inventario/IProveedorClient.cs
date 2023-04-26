using Inventario.Domain.EntityModels;
using Inventario.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Contracts.Inventario
{
    public interface IProveedorClient
    {
        //Get
        Task<List<Proveedor>> GetListaProveedor();

        //Get{id}
        Task<ProveedorViewModel> ObtenerProveedor(int id);

        //Post
        Task GuardarProveedor(Proveedor input);

        //Put
        Task EditarProveedor(Proveedor input);

        //Delete
        Task<bool> EliminarProveedor(int id);
    }
}
