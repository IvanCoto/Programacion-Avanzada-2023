using Inventario.Application.Contracts.Services;
using Inventario.Domain.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.API.Controllers
{
    public class ProductoController : ControllerBase
    {


        public ProductoController(IProductoService service)
        {
            _service = service;
        }
        readonly IProductoService _service;

        [HttpGet]
        [ActionName(nameof(List))]
        public ActionResult<IEnumerable<Producto>> List()
        {
            return _service.List().ToList();
        }

        [HttpGet("{id}")]
        [ActionName(nameof(Get))]
        public ActionResult<Producto> Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        [ActionName(nameof(Post))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult Post(Producto producto)
        {
            _service.Insert(producto);
            return CreatedAtAction(nameof(Get), new { id = producto.Id }, producto);
        }

        [HttpPut]
        [ActionName(nameof(Put))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Put(Producto producto)
        {
            try
            { _service.Update(producto); }
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
            catch (ArgumentNullException)
            { return NotFound(); }

            return NoContent();
        }
    }
}
