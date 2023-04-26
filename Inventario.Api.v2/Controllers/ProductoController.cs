using Inventario.Application.Contracts.Services;
using Inventario.Domain.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Api.v2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {


        public ProductoController(IProductoService service)
        {
            _service = service;
        }
        readonly IProductoService _service;

        [HttpGet]
        [Route("Lista")]
        [ActionName(nameof(List))]
        public ActionResult<IEnumerable<Producto>> List()
        {
            return _service.List().ToList();
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        [ActionName(nameof(Get))]
        public ActionResult<Producto> Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        [Route("Guardar")]
        [ActionName(nameof(Post))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult Post(Producto producto)
        {
            _service.Insert(producto);
            return CreatedAtAction(nameof(Get), new { id = producto.Id }, producto);
        }

        [HttpPut]
        [Route("Editar")]
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
            catch (ArgumentNullException)
            { return NotFound(); }

            return NoContent();
        }
    }
}
