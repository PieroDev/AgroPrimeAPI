using AgroPrimeAPI.Models;
using AgroPrimeAPI.Models.Classes;
using AgroPrimeAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AgroPrimeAPI.Controllers
{
    [ApiController]
    [Route("api/workers")]
    public class WorkerController : ControllerBase
    {

        private readonly IConfiguration config;
        private readonly Helper helper;

        public WorkerController(IConfiguration config)
        {
            this.config = config;
            helper = new Helper(config);
        }

        [HttpGet]
        /// <summary>
        /// Metodo que retorna la lista de clientes en la base de datos
        /// </summary>
        /// <returns>Retorna la lista de clientes</returns>
        public ActionResult GetWorkers()
        {
            try
            {
                using (AgroPrimeContext apDb = new AgroPrimeContext())
                {
                    List<WorkersDTO> workers = apDb.Workers.Select(worker => new WorkersDTO(worker)).ToList();
                    if (workers.Count > 0)
                    {
                        return Ok(new Response()
                        {
                            Data = workers
                        });
                    }
                    else
                    {
                        return NotFound(new Response()
                        {
                            Data = workers,
                            Errors = new List<Error>()
                        {
                            new Error()
                            {
                                Id = 1,
                                Status = "Not Found",
                                Code = 404,
                                Title = "No Data Found",
                                Detail = "There is no data on database."
                            }
                        }
                        });
                    }
                }
            }
            catch (System.Exception err)
            {
                Response response = new Response();
                response.Errors.Add(new Error()
                {
                    Id = 1,
                    Status = "Internal Server Error",
                    Code = 500,
                    Title = err.Message,
                    Detail = err.InnerException != null ? err.InnerException.ToString() : err.Message
                });
                return StatusCode(500, response);
            }
        }


        [HttpGet]
        [Route("getWorker/{numDoc}")]
        public ActionResult GetWorker(string numDoc)
        {
            try
            {
                using(AgroPrimeContext apDb = new AgroPrimeContext())
                {
                    
                    string validateExist= apDb.Workers.Where(x => x.NumDocumento == numDoc)
                                                      .Select(a => a.NumDocumento).FirstOrDefault();

                    if (validateExist != null)
                    {
                        WorkerDTO worker = new WorkerDTO(apDb.Workers.FirstOrDefault(worker => worker.NumDocumento == numDoc));
                        string jsonWorker = JsonSerializer.Serialize(worker);
                        return Ok(new Response()
                        {
                            Data = worker
                        });
                    }
                    else
                    {
                        return NotFound(new Response()
                        {
                            Errors = new List<Error>()
                        {
                            new Error()
                            {
                                Id = 1,
                                Status = "Not Found",
                                Code = 404,
                                Title = "No Data Found",
                                Detail = "Couldn't find the worker"
                            }
                        }
                        });
                    }
                }
            }
            catch (System.Exception err)
            {
                Response response = new Response();
                response.Errors.Add(new Error()
                {
                    Id = 1,
                    Status = "Internal Server Error",
                    Code = 500,
                    Title = err.Message,
                    Detail = err.InnerException != null ? err.InnerException.ToString() : err.Message
                });
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        [Route("addworker")]
        public ActionResult AddWorker([FromBody]Worker newWorker)
        {
            using (AgroPrimeContext apDb = new AgroPrimeContext())
            {
                try
                {
                    List<Error> errors = new List<Error>();
                    string fechaNacimientoString = "";
                    fechaNacimientoString = newWorker.FechaNacimiento.ToString();

                    if (!apDb.Workers.Any(worker => worker.NumDocumento == newWorker.NumDocumento))
                    {
                        if (string.IsNullOrWhiteSpace(newWorker.NumDocumento))
                        {
                            errors.Add(new Error()
                            {
                                Id = 1,
                                Status = "Bad Request",
                                Code = 400,
                                Title = "Invalid Field 'NumDocumento'",
                                Detail = "The field 'NumDocumento' can't be null or white space."
                            });
                        }
                        if (string.IsNullOrWhiteSpace(newWorker.PrimerNombre))
                        {
                            errors.Add(new Error()
                            {
                                Id = 1,
                                Status = "Bad Request",
                                Code = 400,
                                Title = "Invalid Field 'PrimerNombre'",
                                Detail = "The field 'PrimerNombre' can't be null or white space."
                            });
                        }
                        if (string.IsNullOrWhiteSpace(newWorker.PrimerApellido))
                        {
                            errors.Add(new Error()
                            {
                                Id = 1,
                                Status = "Bad Request",
                                Code = 400,
                                Title = "Invalid Field 'PrimerApellido'",
                                Detail = "The field 'PrimerApellido' can't be null or white space."
                            });
                        }
                        if (string.IsNullOrWhiteSpace(newWorker.SegundoApellido))
                        {
                            errors.Add(new Error()
                            {
                                Id = 1,
                                Status = "Bad Request",
                                Code = 400,
                                Title = "Invalid Field 'SegundoApellido'",
                                Detail = "The field 'SegundoApellido' can't be null or white space."
                            });
                        }
                        if (string.IsNullOrWhiteSpace(fechaNacimientoString))
                        {
                            errors.Add(new Error()
                            {
                                Id = 1,
                                Status = "Bad Request",
                                Code = 400,
                                Title = "Invalid Field 'FechaNacimiento'",
                                Detail = "The field 'FechaNacimiento' can't be null or white space."
                            });
                        }

                        if(errors.Count > 0)
                        {
                            return BadRequest(new Response()
                            {
                                Errors = errors
                            });
                        }
                        else
                        {
                            DateTime birthDay = newWorker.FechaNacimiento;
                            Debug.WriteLine(newWorker.FechaNacimiento);
                            bool isAdult = Models.Helper.IsAdult(birthDay);
                            if (!isAdult)
                            {
                                Response response = new Response();
                                response.Errors.Add(new Error()
                                {
                                    Id = 1,
                                    Status = "Bad Request",
                                    Code = 420,
                                    Title = "Age not allowed",
                                    Detail = "The worker you are trying to add is a minor."
                                });
                                return BadRequest(response);
                            }
                            else
                            {
                                apDb.Add(newWorker);
                                apDb.SaveChanges();
                                return Ok(new Response()
                                {
                                    Data = new WorkerDTO(newWorker)
                                });
                            }
                        }
                    }
                    else
                    {
                        Response response = new Response();
                        response.Errors.Add(new Error()
                        {
                            Id = 1,
                            Status = "Bad Request",
                            Code = 400,
                            Title = "The worker already exists",
                            Detail = "The NumDocumento that you are trying to add is already on the database."
                        });
                        return BadRequest(response);
                    }
                }
                catch (Exception err)
                {
                    Response response = new Response();
                    response.Errors.Add(new Error()
                    {
                        Id = 1,
                        Status = "Internal Server Error",
                        Code = 500,
                        Title = err.Message,
                        Detail = err.InnerException != null ? err.InnerException.ToString() : err.Message
                    });
                    return StatusCode(500, response);
                }
            }
        }

        [HttpPut]
        [Route("update/{numDoc}")]
        public ActionResult UpdateWorker(string numDoc, [FromBody]Worker updatedWorker)
        {
            try
            {
                List<Error> errors = new List<Error>();
                string fechaNacimientoString = "";
                fechaNacimientoString = updatedWorker.FechaNacimiento.ToString();

                using (AgroPrimeContext apDb = new AgroPrimeContext())
                {
                    string validateExist = apDb.Workers.Where(x => x.NumDocumento == numDoc)
                                                      .Select(a => a.NumDocumento).FirstOrDefault();

                    if(validateExist != null)
                    {
                        Worker currentWorker = apDb.Workers.FirstOrDefault(worker => worker.NumDocumento == numDoc);


                        if (updatedWorker.NumDocumento != numDoc)
                        {
                            return NotFound(new Response()
                            {
                                Errors = new List<Error>()
                                {
                                    new Error()
                                    {
                                        Id = 1,
                                        Status = "Rejected",
                                        Code = 509,
                                        Title = "Worker update rejected",
                                        Detail = "numDocument is inmutable"
                                    }
                                }
                            });
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(updatedWorker.PrimerNombre))
                            {
                                errors.Add(new Error()
                                {
                                    Id = 1,
                                    Status = "Bad Request",
                                    Code = 400,
                                    Title = "Invalid Field 'PrimerNombre'",
                                    Detail = "The field 'PrimerNombre' can't be null or white space."
                                });
                            }
                            else
                            {
                                currentWorker.PrimerNombre = updatedWorker.PrimerNombre;
                            }

                            if (!string.IsNullOrWhiteSpace(updatedWorker.SegundoNombre))
                            {
                                currentWorker.SegundoNombre = updatedWorker.SegundoNombre;
                            }

                            if (string.IsNullOrWhiteSpace(updatedWorker.PrimerApellido))
                            {
                                errors.Add(new Error()
                                {
                                    Id = 1,
                                    Status = "Bad Request",
                                    Code = 400,
                                    Title = "Invalid Field 'PrimerApellido'",
                                    Detail = "The field 'PrimerApellido' can't be null or white space."
                                });
                            }
                            else
                            {
                                currentWorker.PrimerApellido = updatedWorker.PrimerApellido;
                            }

                            if (string.IsNullOrWhiteSpace(updatedWorker.SegundoApellido))
                            {
                                errors.Add(new Error()
                                {
                                    Id = 1,
                                    Status = "Bad Request",
                                    Code = 400,
                                    Title = "Invalid Field 'SegundoApellido'",
                                    Detail = "The field 'SegundoApellido' can't be null or white space."
                                });
                            }
                            else
                            {
                                currentWorker.SegundoApellido = updatedWorker.SegundoApellido;
                            }

                            if (string.IsNullOrWhiteSpace(fechaNacimientoString))
                            {
                                errors.Add(new Error()
                                {
                                    Id = 1,
                                    Status = "Bad Request",
                                    Code = 400,
                                    Title = "Invalid Field 'FechaNacimiento'",
                                    Detail = "The field 'FechaNacimiento' can't be null or white space."
                                });
                            }
                            else
                            {
                                currentWorker.FechaNacimiento = updatedWorker.FechaNacimiento;
                            }

                            if(errors.Count > 0)
                            {
                                return BadRequest(new Response()
                                {
                                    Errors = errors
                                });
                            }
                            apDb.SaveChanges();
                            return Ok(new Response()
                            {
                                Data = new WorkerDTO(currentWorker),
                                Errors = errors
                            });
                            
                        }
                    }
                    else
                    {
                        return NotFound(new Response()
                        {
                            Errors = new List<Error>()
                        {
                            new Error()
                            {
                                Id = 1,
                                Status = "Not Found",
                                Code = 404,
                                Title = "No Data Found",
                                Detail = "Couldn't find the worker"
                            }
                        }
                        });
                    }
                }
            }
            catch (Exception err)
            {
                Response response = new Response();
                response.Errors.Add(new Error()
                {
                    Id = 1,
                    Status = "Internal Server Error",
                    Code = 500,
                    Title = err.Message,
                    Detail = err.InnerException != null ? err.InnerException.ToString() : err.Message
                });
                return StatusCode(500, response);
            }
        }

        [HttpDelete]
        [Route("delete/{numDoc}")]
        public ActionResult DeleteWorker(string numDoc)
        {
            try
            {
                List<Error> errors = new List<Error>();

                using (AgroPrimeContext apDb = new AgroPrimeContext())
                {
                    string validateExist = apDb.Workers.Where(x => x.NumDocumento == numDoc)
                                                      .Select(a => a.NumDocumento).FirstOrDefault();

                    if (validateExist != null)
                    {
                        Worker currentWorker = apDb.Workers.FirstOrDefault(worker => worker.NumDocumento == numDoc);

                        apDb.Remove(currentWorker);
                        apDb.SaveChanges();
                        return Ok(new Response()
                        {
                            Data = new { deletedNumDoc = numDoc }
                        });
                    }
                    else
                    {
                        return NotFound(new Response()
                        {
                            Errors = new List<Error>()
                        {
                            new Error()
                            {
                                Id = 1,
                                Status = "Not Found",
                                Code = 404,
                                Title = "No Data Found",
                                Detail = "Couldn't find the worker"
                            }
                        }
                        });
                    }
                }
            }
            catch (Exception err)
            {
                Response response = new Response();
                response.Errors.Add(new Error()
                {
                    Id = 1,
                    Status = "Internal Server Error",
                    Code = 500,
                    Title = err.Message,
                    Detail = err.InnerException != null ? err.InnerException.ToString() : err.Message
                });
                return StatusCode(500, response);
            }
        }




    }
}
