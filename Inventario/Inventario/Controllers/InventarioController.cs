using Microsoft.AspNetCore.Mvc;
using Inventario.Models;
using Inventario.Services;

namespace Inventario.Controllers
{
    [ApiController]
    [Route("api/inventario")]

    public class InventarioController : Controller
    {
        public InventarioController(InventarioServices services)
        {
            _services = services;
        }
        private InventarioServices _services;

        [HttpGet("PorId/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var record = _services.GetInventarios(id);

                return Ok(record);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [HttpGet("Itens")]
        public IEnumerable<ItemRecord> Get()
        {
            return _services.GetItensList();
        }

        [HttpGet("Filtros")]
        public IActionResult Get(
            int? id,
            int? idPersonagem,
            string? descricao,
            int? quantidade
                            )
        {
            try
            {
                var records = _services.GetFilter(id, idPersonagem, descricao, quantidade);

                return Ok(records);
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPost("Item")]
        public IActionResult Create([FromBody] ItemRecord record)
        {
            try
            {
                _services.CreateItem(record);

                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
