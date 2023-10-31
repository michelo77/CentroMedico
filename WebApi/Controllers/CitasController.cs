using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static WebApi.Model.AppDbContext;
using WebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        //acceso a la BD
        private readonly AppDbContext _context;

        //Constructor => __init__(self)
        public CitasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet] //Obtener o leer de la base de datos
        [Route("getAll")] //Obtener todas las Citas medicas
        public async Task<IActionResult> getAll()
        {
            //Select * From Citas
            var citas = await _context.TblCitas.ToListAsync();
            return Ok(citas);
        }

        [HttpPost] //Escribir en la base de datos
        [Route("AddCita")] //Añadir Citas
        public async Task<IActionResult> AddCita(Citas cita)
        {
            //Insert into departamentos 

            var existe = await _context.TblProfesionales
                .Where(x => x.Id.Equals(cita.Id))
                .FirstOrDefaultAsync();
            if (existe == null)
            {
                _context.Add(cita);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest("cita ya está registrada!");
        }

        [HttpPut]
        [Route("upCitas")]
        public async Task<IActionResult> upCitas(int id, Citas cita)
        {
            var existe = await _context.TblCitas
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (existe == null)
            {
                return NotFound();
            }
            existe.Fecha = cita.Fecha;
            existe.Estado = cita.Estado;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("delCitas")]
        public async Task<IActionResult> delCita(int id)
        {
            var existe = await _context.TblCitas
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (existe == null)
            {
                return NotFound();
            }

            _context.Remove(existe);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
