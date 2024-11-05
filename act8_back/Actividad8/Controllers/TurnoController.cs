using Actividad8.Negocio.Data;
using Actividad8.Negocio.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Actividad8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {

        private IRepositoryTurno repositoryTurno;

        public TurnoController(IRepositoryTurno _repository)
        {
            repositoryTurno = _repository;
        }

        // GET: api/<TurnoController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(repositoryTurno.GetTurnos());
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }

        // GET api/<TurnoController>/5
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            try
            {
                return Ok(repositoryTurno.TraerTurnosConId(id));
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }

        // POST api/<TurnoController>
        [HttpPost]
        public IActionResult Post([FromBody] TTurno value)
        {
            try
            {
                repositoryTurno.AgregarTurno(value);
                return Ok("Turno creado con éxito");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }

        // PUT api/<TurnoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TTurno value)
        {
            try
            {
                if (id == null)
                    return BadRequest("Identificador de turno inválido");

                repositoryTurno.ActualizarTurno(value, id);
                return Ok("Turno actualizado exitosamente");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }

        // DELETE api/<TurnoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                repositoryTurno.EliminarTurno(id);
                return Ok("Turno eliminado");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }
    }
}
