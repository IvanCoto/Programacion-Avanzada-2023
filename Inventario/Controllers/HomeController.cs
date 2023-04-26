using Inventario.Application.Contracts.DbContexts;
using Inventario.Application.Contracts.Inventario;
using Inventario.Application.Contracts.Services;
using Inventario.Domain.EntityModels;
using Inventario.Domain.InputModels;
using Inventario.Domain.ViewModels;
using Inventario.Infraestructure.Implementations;
using Inventario.Models;
using Inventario.Persistence.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Diagnostics;

namespace Inventario.Controllers
{
    public class HomeController : Controller
    {

        public HomeController
            (ILogger<HomeController> logger, IClienteClient clienteClient,
            IProductoClient productoClient, IProveedorClient proveedorClient

            ,IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _logger = logger;
            _clienteClient = clienteClient;
            _productoClient = productoClient;
            _proveedorClient = proveedorClient;
            _unitOfWork = unitOfWork;
            _repositoryProveedor = _unitOfWork.Repository<Proveedor>();
            _repositoryProducto = _unitOfWork.Repository<Producto>();
            _repositoryCliente = _unitOfWork.Repository<Cliente>();
            /*_clienteService = clienteService;*/
        }

        readonly ILogger<HomeController> _logger;

        //Conexion a API de inventario
        readonly IClienteClient _clienteClient;
        readonly IProductoClient _productoClient;
        readonly IProveedorClient _proveedorClient;

        //Manejo de datos de vistas
        readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        readonly IRepository<Proveedor> _repositoryProveedor;
        readonly IRepository<Producto> _repositoryProducto;
        readonly IRepository<Cliente> _repositoryCliente;

        /*readonly IClienteService _clienteService;*/




        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //-------Vista de Clientes-------------------------------------------------------------------------------------------------
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Clientes()
        {
            

            List<Cliente> ListaCliente = await _clienteClient.GetListaCliente();

            return View(ListaCliente);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> upsertCliente(int id)
        {
            var cliente = new ClienteViewModel
            {
                Cliente =
                id == 0
                    ? new Cliente()
                    : _repositoryCliente.Get(s => s.Id == id),
                GetClientes = _repositoryCliente.GetAll().ToList()
            };

            ViewBag.Action = "Nuevo Cliente";
            


            if (id != 0)
            {
                cliente = await _clienteClient.ObtenerCliente(id);
                ViewBag.Action = "Editar Cliente";
            }
            return View(cliente);
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> guardarCambiosCliente(ClienteViewModel model)
        {
            if (ModelState.IsValid)
            {
                Cliente input = model.Cliente;

                try
                {
                    if (input.Id == 0)
                    {
                        await _clienteClient.GuardarCliente(input);
                    }
                    else
                    {
                        await _clienteClient.EditarCliente(input);
                    }
                    _unitOfWork.Save();

                    return RedirectToAction("Clientes");
                }
                catch
                {
                    ModelState.AddModelError("", "Internal Server Error.");
                }
            }
            return View(model);

            //bool respuesta;
            //if (cliente.Id == 0)
            //{
            //    respuesta = await _clienteClient.GuardarCliente(cliente);
            //}
            //else
            //{
            //    respuesta = await _clienteClient.EditarCliente(cliente);
            //}

            //if (respuesta)
            //{
            //    return RedirectToAction("Clientes");
            //}
            //else
            //{
            //    return NoContent();
            //}
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> eliminarCliente(int id)
        {
            var respuesta = await _clienteClient.EliminarCliente(id);
            if (respuesta)
            {
                return RedirectToAction("Clientes");
            }
            else
            {
                return NoContent();
            }
        }
        //-------Vista de Proveedores-------------------------------------------------------------------------------------------------
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Proveedores()
        {

            List<Proveedor> ListaProveedor = await _proveedorClient.GetListaProveedor();

            return View(ListaProveedor);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> upsertProveedor(int id)
        {
            var model = new ProveedorViewModel
            {
                Proveedor =
                id == 0
                    ? new Proveedor()
                    : _repositoryProveedor.Get(s => s.Id == id)
            };

            //Proveedor proveedor = new Proveedor();

            ViewBag.Action = "Nuevo Proveedor";

            if (id != 0)
            {
                model = await _proveedorClient.ObtenerProveedor(id);
                ViewBag.Action = "Editar Proveedor";
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> guardarCambiosProveedor(ProveedorViewModel model)
        {

            if (ModelState.IsValid)
            {
                Proveedor input = model.Proveedor;

                try
                {
                    if (input.Id == 0)
                    {
                        await _proveedorClient.GuardarProveedor(input);
                    }
                    else
                    {
                        await _proveedorClient.EditarProveedor(input);
                    }
                    _unitOfWork.Save();

                    return RedirectToAction("Proveedores");
                }
                catch
                {
                    ModelState.AddModelError("", "Internal Server Error.");
                }
            }

            return View(model);
            //if (proveedor.Id == 0)
            //{
            //    respuesta = await _proveedorClient.GuardarProveedor(proveedor);
            //}
            //else
            //{
            //    respuesta = await _proveedorClient.EditarProveedor(proveedor);
            //}

            //if (respuesta)
            //{
            //    return RedirectToAction("Proveedor");
            //}
            //else
            //{
            //    return NoContent();
            //}

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> eliminarProveedor(int id)
        {
            var respuesta = await _proveedorClient.EliminarProveedor(id);
            if (respuesta)
            {
                return RedirectToAction("Proveedores");
            }
            else
            {
                return NoContent();
            }
        }



        //-------Vista de Productos-------------------------------------------------------------------------------------------------
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Productos()
        {


            List<Producto> ListaProducto = await _productoClient.GetListaProducto();

            return View(ListaProducto);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> upsertProducto(int id)
        {
            var model = new ProductoViewModel
            {
                Producto =
                id == 0
                    ? new Producto()
                    : _repositoryProducto.Get(s => s.Id == id),
                GetProveedores = _repositoryProveedor.GetAll().ToList()
            };


            //Producto producto = new Producto();

            ViewBag.Action = "Nuevo Producto";

            if (id != 0)
            {
                model = await _productoClient.ObtenerProducto(id);
                ViewBag.Action = "Editar Producto";
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> guardarCambiosProducto(ProductoViewModel model)
        {

            //bool respuesta;
            //if (producto.Id == 0)
            //{
            //    respuesta = await _productoClient.GuardarProducto(producto);
            //}
            //else
            //{
            //    respuesta = await _productoClient.EditarProducto(producto);
            //}

            //if (respuesta)
            //{
            //    return RedirectToAction("Productos");
            //}
            //else
            //{
            //    return NoContent();
            //}

            if (ModelState.IsValid)
            {
                Producto input = model.Producto;

                try
                {
                    if (input.Id == 0)
                    {
                        await _productoClient.GuardarProducto(input);
                    }
                    else
                    {
                        await _productoClient.EditarProducto(input);
                    }
                    _unitOfWork.Save();

                    return RedirectToAction("Productos");
                }
                catch
                {
                    ModelState.AddModelError("", "Internal Server Error.");
                }
            }

            return View(model);
        

    }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> eliminarProducto(int id)
        {
            var respuesta = await _productoClient.EliminarProducto(id);
            if (respuesta)
            {
                return RedirectToAction("Productos");
            }
            else
            {
                return NoContent();
            }
        }


        //------ResponseCache-------------------------------------------------------------------------------------------------------
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}