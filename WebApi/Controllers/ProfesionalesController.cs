using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Model;
using static WebApi.Model.AppDbContext;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesionalesController : ControllerBase
    {
        //acceso a la BD
        private readonly AppDbContext _context;

        //Constructor => __init__(self)
        public ProfesionalesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet] //Obtener o leer de la base de datos
        [Route("getAll")] //Obtener todos los Profesionales
        public async Task<IActionResult> getAll()
        {
            //Select * From Profesionales
            var profesional = await _context.TblProfesionales.ToListAsync();
            return Ok(profesional);
        }

        [HttpPost] //Escribir en la base de datos
        [Route("AddProfesionales")] //Añadir Profesionales
        public async Task<IActionResult> AddProfesionales(Profesionales profesional)
        {
            //Insert into departamentos 

            var existe = await _context.TblProfesionales
                .Where(x => x.Nombre.Equals(profesional.Nombre))
                .FirstOrDefaultAsync();
            if (existe == null)
            {
                _context.Add(profesional);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest("Profesional ya está registrado!");
        }

        [HttpPut]
        [Route("upProfesionales")]
        public async Task<IActionResult> uptDpto(int id, Profesionales profesional)
        {
            var existe = await _context.TblProfesionales
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (existe == null)
            {
                return NotFound();
            }
            existe.Rut = profesional.Rut;
            existe.Nombre = profesional.Nombre;
            existe.Apellido = profesional.Apellido;
            existe.Correo = profesional.Correo;
            existe.Telefono = profesional.Telefono;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("delProfesinoal")]
        public async Task<IActionResult> delProfesional(int id)
        {
            var existe = await _context.TblProfesionales
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
