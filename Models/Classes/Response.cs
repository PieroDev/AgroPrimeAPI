using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroPrimeAPI.Models.Classes
{
    public class Response
    {
        public object Data { get; set; }
        public List<Error> Errors { get; set; }
        public object Meta { get; set; }

        public Response()
        {
            Errors = new List<Error>();
            Meta = new
            {
                copyright = "Copyright 2022 | Piero Zúñiga",
                author = "Piero Zúñiga",
                PostMan = "https://documenter.getpostman.com/view/13100703/UVeNo3SY"
            };
        }
    }
}
