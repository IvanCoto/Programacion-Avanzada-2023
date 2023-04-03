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
        [ActionName(nameof(List))]
        public ActionResult<IEnumerable<Proveedor>> List()
        {
            return _service.List().ToList();
        }

        [HttpGet("{id}")]
        [ActionName(nameof(Get))]
        public ActionResult<Proveedor> Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        [ActionName(nameof(Post))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult Post(Proveedor proveedor)
        {
            _service.Insert(proveedor);
            return CreatedAtAction(nameof(Get), new { id = proveedor.Id }, proveedor);
        }

        [HttpPut]
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

        [HttpDelete("{id}")]
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
