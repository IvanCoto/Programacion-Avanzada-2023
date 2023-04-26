using Inventario.Application.Contracts.Services;
using Inventario.Domain.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Inventario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        public ProveedorController(IProveedorService service)
        {
            _service = service;
        }

        readonly IProveedorService _service;

        [HttpGet]
        [Route("Lista")]
        [ActionName(nameof(List))]
        public ActionResult<IEnumerable<Proveedor>> List()
        {
            return _service.List().ToList();
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        [ActionName(nameof(Get))]
        public ActionResult<Proveedor> Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        [Route("Guardar")]
        [ActionName(nameof(Post))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult Post(Proveedor proveedor)
        {
            _service.Insert(proveedor);
            return CreatedAtAction(nameof(Get), new { id = proveedor.Id }, proveedor);
        }

        [HttpPut]
        [Route("Editar")]
        [ActionName(nameof(Put))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Put(Proveedor proveedor)
        {
            try
            { _service.Update(proveedor); }
            catch (DbUpdateConcurrencyException)
            { return NotFound(); }

            return NoContent();
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        [ActionName(nameof(Delete))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Delete(int id)
        {
            try
            { _service.Delete(id); }
            catch (DbUpdateConcurrencyException)
            { return NotFound(); }

            return NoContent();
        }
    }
}
