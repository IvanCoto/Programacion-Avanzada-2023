using Inventario.Application.Contracts.DbContexts;
using Inventario.Application.Contracts.Inventario;
using Inventario.Domain.EntityModels;
using Inventario.Domain.ViewModels;
using Inventario.Persistence.DbContexts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Inventario.Infraestructure.Implementations
{
    public class ClienteClient : IClienteClient
    {
        const string CLIENTE_BASE_ADDRESS = "https://localhost:7203/api/Cliente";

        public ClienteClient(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repositoryCliente = _unitOfWork.Repository<Cliente>();
        }
        readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;

        readonly IRepository<Cliente> _repositoryCliente;



        JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true } ;
        public async Task<List<Cliente>> GetListaCliente()
        {
            List<Cliente> ListaCliente = new List<Cliente>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CLIENTE_BASE_ADDRESS);
                var response = await client.GetAsync("Cliente/Lista");
                if (response.IsSuccessStatusCode)
                {
                    var json_respuesta = await response.Content.ReadAsStringAsync();
                    var clientes = JsonConvert.DeserializeObject<List<Cliente>>(json_respuesta);
                    ListaCliente = clientes;
                }
                return ListaCliente;
            }
        }

        public async Task<ClienteViewModel> ObtenerCliente(int id)
        {

            var obtenerCliente = new ClienteViewModel
            {
                Cliente =
                id == 0
                    ? new Cliente()
                    : _repositoryCliente.Get(s => s.Id == id)
            };


            //Cliente obtenerCliente = new Cliente();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CLIENTE_BASE_ADDRESS);
                var response = await client.GetAsync($"Cliente/Obtener/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json_respuesta = await response.Content.ReadAsStringAsync();
                    var clientes = JsonConvert.DeserializeObject<ClienteViewModel>(json_respuesta);
                    obtenerCliente = clientes;
                }
                return obtenerCliente;
            }
        }

        public async Task GuardarCliente(Cliente input)
        {
            //bool guardarCliente = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CLIENTE_BASE_ADDRESS);

                var content = new StringContent
                    (JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"Cliente/Guardar", content);
                if (response.IsSuccessStatusCode)
                {
                return;

                }
            }
        }

        public async Task EditarCliente(Cliente input)
        {
            //bool editarCliente = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CLIENTE_BASE_ADDRESS);

                var content = new StringContent
                    (JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"Cliente/Editar", content);
                if (response.IsSuccessStatusCode)
                {
                return;
                }
            }
        }

        public async Task<bool> EliminarCliente(int id)
        {
            bool eliminarCliente = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CLIENTE_BASE_ADDRESS);


                var response = await client.DeleteAsync($"Cliente/Eliminar/{id}");
                if (response.IsSuccessStatusCode)
                {
                    eliminarCliente = true;
                }
                return eliminarCliente;
            }
        }
    }
}
