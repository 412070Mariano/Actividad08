using Actividad8.Negocio.Data;
using Actividad8.Negocio.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Actividad8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private IRepositoryServicio repositoryServicio;

        public ServicioController(IRepositoryServicio _repositoryServicio)
        {
            repositoryServicio = _repositoryServicio;
        }

        // GET: api/<ValuesController>
        [HttpGet]

        public IActionResult GetAll()
        {
            try
            {
                return Ok(repositoryServicio.GetServicios());
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult GetByFilter(int id)
        {
            try
            {
                return Ok(repositoryServicio.TraerServicio(id));
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }

        [HttpGet("/Servicio/Filter")]
        public IActionResult GeByFilter([FromQuery] string? nombre, [FromQuery] string? enPromo)
        {
            try
            {
                return Ok(repositoryServicio.TraerServicioFiltrado(nombre, enPromo));
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody]  TServicio value)
        {
            try
            {
                repositoryServicio.AgregarServicio(value);
                return Ok("Servicio creado con éxito");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] TServicio servicio, int id)
        {
            try
            {
                if (id == null)
                    return BadRequest("Identificador de servicio inválido");

                repositoryServicio.ActualizarServicio(servicio, id);
                return Ok("Servicio actualizado exitosamente");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                repositoryServicio.EliminarServicio(id);
                return Ok("Servicio eliminado");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }
    }
}
