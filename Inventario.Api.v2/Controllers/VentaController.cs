using Inventario.Application.Contracts.Services;
using Inventario.Domain.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Inventario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        
        public VentaController(IVentaService service)
        {
            _service = service;
        }

        readonly IVentaService _service;

        [HttpGet]
        [ActionName(nameof(List))]
        public ActionResult<IEnumerable<Venta>> List()
        {
            return _service.List().ToList();
        }

        [HttpGet("{id}")]
        [ActionName(nameof(Get))]
        public ActionResult<Venta> Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        [Route("Guardar")]
        [ActionName(nameof(Post))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult Post(Venta venta)
        {
            _service.Insert(venta);
            return CreatedAtAction(nameof(Get), new { id = venta.Id }, venta);
        }
    }
}
