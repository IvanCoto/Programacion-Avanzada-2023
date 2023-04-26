using Inventario.Application.Contracts.DbContexts;
using Inventario.Application.Contracts.Inventario;
using Inventario.Domain.EntityModels;
using Inventario.Domain.ViewModels;
using Inventario.Persistence.DbContexts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Inventario.Infraestructure.Implementations
{
    public class ProductoClient : IProductoClient
    {


        public ProductoClient(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repositoryProducto = _unitOfWork.Repository<Producto>();
            _repositoryProveedor = _unitOfWork.Repository<Proveedor>();
        }
        readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        readonly IRepository<Producto> _repositoryProducto;
        readonly IRepository<Proveedor> _repositoryProveedor;


        const string PRODUCTO_BASE_ADDRESS = "https://localhost:7203/api/Producto";

        public async Task<List<Producto>> GetListaProducto()
        {
            List<Producto> ListaProducto = new List<Producto>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(PRODUCTO_BASE_ADDRESS);
                var response = await client.GetAsync("Producto/Lista");


                if (response.IsSuccessStatusCode)
                {
                    var json_respuesta = await response.Content.ReadAsStringAsync();
                    var productos = JsonConvert.DeserializeObject<List<Producto>>(json_respuesta);
                    ListaProducto = productos;
                }
                return ListaProducto;
            }
        }

        public async Task<ProductoViewModel> ObtenerProducto(int id)
        {
            var model = new ProductoViewModel
            {
                Producto =
                id == 0
                    ? new Producto()
                    : _repositoryProducto.Get(s => s.Id == id),
                GetProveedores = _repositoryProveedor.GetAll().ToList()
            };

            //Producto obtenerProducto = new Producto();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(PRODUCTO_BASE_ADDRESS);
                var response = await client.GetAsync($"Producto/Obtener/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json_respuesta = await response.Content.ReadAsStringAsync();
                    var product = JsonConvert.DeserializeObject<ProductoViewModel>(json_respuesta);
                    model = product;
                }
                return model;
            }
        }
        public async Task GuardarProducto(Producto input)
        {
            
            //bool guardarProducto = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(PRODUCTO_BASE_ADDRESS);

                var content = new StringContent
                    (JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"Producto/Guardar", content);
                if (response.IsSuccessStatusCode)
                {
                    return;
                }

            }
        }

        public async Task EditarProducto(Producto input)
        {
            
            //bool editarProducto = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(PRODUCTO_BASE_ADDRESS);

                var content = new StringContent
                    (JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

                var response = await client.PutAsync("Producto/Editar", content);
                if (response.IsSuccessStatusCode)
                {
                    return;

                }
            }
        }

        public async Task<bool> EliminarProducto(int id)
        {
            bool eliminarProducto = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(PRODUCTO_BASE_ADDRESS);


                var response = await client.DeleteAsync($"Producto/Eliminar/{id}");
                if (response.IsSuccessStatusCode)
                {
                    eliminarProducto = true;
                }
                return eliminarProducto;
            }
        }



    }
}
