using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroPrimeAPI.Models.DTO
{
    public partial class WorkerDTO
    {
        public int Id { get; set; }
        public string NumDocumento { get; set; }
        public string FechaNacimiento { get; set; }
        public string NombreCompleto { get; set; }

        public WorkerDTO() 
        { 
        }

        public WorkerDTO(AgroPrimeAPI.Models.Worker worker)
        {
            this.Id = worker.Id;
            this.NumDocumento = worker.NumDocumento;
            this.FechaNacimiento = worker.FechaNacimiento.ToString("dd-MM-yyyy");
            this.NombreCompleto = $"{worker.PrimerNombre} {worker.SegundoNombre} {worker.PrimerApellido} {worker.SegundoApellido}";
        }


    }
}
