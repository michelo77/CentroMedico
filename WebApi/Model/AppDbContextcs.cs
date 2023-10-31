using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WebApi.Model.AppDbContext;

namespace WebApi.Model
{
    // Class de conección a la base de datos.
    public class AppDbContext : DbContext
    {
        // Tablas de la DB
        public DbSet<Pacientes> TblPacientes { get; set; }
        public DbSet<Citas> TblCitas { get; set; }
        public DbSet<Pagos> TblPagos { get; set; }
        public DbSet<Profesionales> TblProfesionales { get; set; }
        public DbSet<Especialidades> TblEspecialidades { get; set; }
        public DbSet<ProfesionalEspecialidad> TblProfesionalEspecialidad { get; set; }



        // Fin Tablas

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CentroMedicoDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        }

        public class Profesionales
        {
            [Key]
            public int Id { get; set; }
            public string Rut { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Correo { get; set; }
            public string Telefono { get; set; }

        }
        public class Pacientes
        {
            [Key]
            public int Id { get; set; }
            public string Rut { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Direccion { get; set; }
            public string Telefono { get; set; }
            public string Correo { get; set; }
            public string fechaNacimiento { get; set; }

        }

        public class Citas
        {
            [Key]
            public int Id { get; set; }
            public string Fecha { get; set; }
            public string Estado { get; set; }

            // Clave foránea para la relación con Paciente
            [ForeignKey("Pacientes")]
            public int PacienteId { get; set; }
            public Pacientes Pacientes { get; set; }

            // Clave foránea para la relación con Profesional
            [ForeignKey("Profesionales")]
            public int ProfesionalId { get; set; }
            public Profesionales Profesionales { get; set; }
        }



        public class Pagos
        {
            [Key]
            public int Id { get; set; }
            public int Monto { get; set; }

            //Clave Foranea
            [ForeignKey("Citas")]
            public int CitasId { get; set; }
            public Citas Citas { get; set; }
        }

        public class Especialidades
        {
            [Key]
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
        }

        public class ProfesionalEspecialidad
        {
            [Key]
            public int Id { get; set; }

            //Claves foráneas
            [ForeignKey("Especialidades")]
            public int EspecialidadId { get; set; }
            public Especialidades Especialidades { get; set; }

            [ForeignKey("Profecionales")]
            public int ProfecionalId { get; set; }
            public Profesionales Profesionales { get; set; }

        }
    }

}