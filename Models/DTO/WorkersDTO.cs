using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroPrimeAPI.Models.DTO
{
    public class WorkersDTO
    {
        public int Id { get; set; }
        public string NumDocumento { get; set; }
        public string NombreCompleto { get; set; }
        public string FechaNacimiento { get; set; }


        public WorkersDTO()
        {
        }

        public WorkersDTO(AgroPrimeAPI.Models.Worker worker)
        {
            this.Id = worker.Id;
            this.NumDocumento = worker.NumDocumento;
            if (worker.SegundoNombre != null)
            {
                this.NombreCompleto = $"{worker.PrimerNombre} {worker.SegundoNombre} {worker.PrimerApellido} {worker.SegundoApellido}";
            }
            else
            {
                this.NombreCompleto = $"{worker.PrimerNombre} {worker.PrimerApellido} {worker.SegundoApellido}";
            }
            this.FechaNacimiento = worker.FechaNacimiento.ToString("dd-MM-yyyy");
        }
    }
}
