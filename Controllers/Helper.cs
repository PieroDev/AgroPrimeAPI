using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AgroPrimeAPI.Models
{
    public class Helper
    {
        private readonly IConfiguration config;

        private readonly IWebHostEnvironment hostEnviroment;

        public Helper(IConfiguration config)
        {
            this.config = config;
        }

        public Helper(IConfiguration config, IWebHostEnvironment hostEnviroment)
        {
            this.config = config;
            this.hostEnviroment = hostEnviroment;
        }

        public static bool IsAdult(DateTime birthday)
        {
            DateTime today = DateTime.Now;
            double totalDays = (today - birthday).TotalDays;
            double totalYears = totalDays / 365;
            Debug.WriteLine(totalYears);
            if(totalYears > 18)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }

    
}
