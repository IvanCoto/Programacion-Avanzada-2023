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
    public class ProveedorClient : IProveedorClient
    {
        const string PROVEEDOR_BASE_ADDRESS = "https://localhost:7203/api/Proveedor";

        public ProveedorClient(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repositoryProveedor = _unitOfWork.Repository<Proveedor>();
        }
        readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;

        readonly IRepository<Proveedor> _repositoryProveedor;


        JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public async Task<List<Proveedor>> GetListaProveedor()
        {
            List<Proveedor> ListaProveedor = new List<Proveedor>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(PROVEEDOR_BASE_ADDRESS);
                var response = await client.GetAsync("Proveedor/Lista");
                if (response.IsSuccessStatusCode)
                {
                    var json_respuesta = await response.Content.ReadAsStringAsync();
                    var proveedores = JsonConvert.DeserializeObject<List<Proveedor>>(json_respuesta);
                    ListaProveedor = proveedores;
                }
                return ListaProveedor;
            }
        }
        public async Task<ProveedorViewModel> ObtenerProveedor(int id)
        {
            var model = new ProveedorViewModel
            {
                Proveedor =
                id == 0
                    ? new Proveedor()
                    : _repositoryProveedor.Get(s => s.Id == id)
            };

            //Proveedor obtenerProveedor = new Proveedor();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(PROVEEDOR_BASE_ADDRESS);
                var response = await client.GetAsync($"Proveedor/Obtener/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json_respuesta = await response.Content.ReadAsStringAsync();
                    var proveedores = JsonConvert.DeserializeObject<ProveedorViewModel>(json_respuesta);
                    model = proveedores;
                }
                return model;
            }
        }
        public async Task GuardarProveedor(Proveedor input)
        {

            //bool respuesta = false;



            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(PROVEEDOR_BASE_ADDRESS);

                var content = new StringContent
                    (JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"Proveedor/Guardar", content);
                if (response.IsSuccessStatusCode)
                {
                    //var json_respuesta = await response.Content.ReadAsStringAsync();
                    //var resultado = JsonConvert.DeserializeObject<ProveedorViewModel>(json_respuesta);
                    //input = resultado.input;
                    return;

                }
                //return input
            }
        }

        public async Task EditarProveedor(Proveedor input)
        {
            
            //bool respuesta = false;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(PROVEEDOR_BASE_ADDRESS);

                var content = new StringContent
                    (JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"Proveedor/Editar", content);
                if (response.IsSuccessStatusCode)
                {
                return;

                }
            }
        }

        public async Task<bool> EliminarProveedor(int id)
        {
            bool eliminarProveedor = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(PROVEEDOR_BASE_ADDRESS);


                var response = await client.DeleteAsync($"Proveedor/Eliminar/{id}");
                if (response.IsSuccessStatusCode)
                {
                    eliminarProveedor = true;
                }
                return eliminarProveedor;
            }
        }



    }
}
