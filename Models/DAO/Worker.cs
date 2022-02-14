using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroPrimeAPI.Models
{
    public class Worker
    {
        public Worker()
        {
        }

        public Worker(int id, string numDocumento, string primerNombre, string segundoNombre, string primerApellido, string segundoApellido, DateTime fechaNacimiento)
        {
            Id = id;
            NumDocumento = numDocumento;
            PrimerNombre = primerNombre;
            SegundoNombre = segundoNombre;
            PrimerApellido = primerApellido;
            SegundoApellido = segundoApellido;
            FechaNacimiento = fechaNacimiento;
        }

        public int Id { get; set; }
        public string NumDocumento { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }


    }
}
