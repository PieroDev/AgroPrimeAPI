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
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }


        public WorkerDTO() 
        { 
        }

        public WorkerDTO(AgroPrimeAPI.Models.Worker worker)
        {
            this.Id = worker.Id;
            this.NumDocumento = worker.NumDocumento;
            this.PrimerNombre = worker.PrimerNombre;
            this.SegundoNombre = worker.SegundoNombre;
            this.PrimerApellido = worker.PrimerApellido;
            this.SegundoApellido = worker.SegundoApellido;
            this.FechaNacimiento = worker.FechaNacimiento;
        }
    }
}
