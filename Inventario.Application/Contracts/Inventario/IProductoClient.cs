using Inventario.Domain.EntityModels;
using Inventario.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Contracts.Inventario
{
    public interface IProductoClient
    {
        //Get
        Task<List<Producto>> GetListaProducto();

        //Get{id}
        Task<ProductoViewModel> ObtenerProducto(int id);

        //Post
        Task GuardarProducto(Producto input);

        //Put
        Task EditarProducto(Producto input);

        //Delete
        Task<bool> EliminarProducto(int id);
    }
}
