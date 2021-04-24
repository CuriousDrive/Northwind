using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Northwind.Data;

namespace Northwind_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching","Very Hot"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public NorthwindContext _context { get; }

        public WeatherForecastController(ILogger<WeatherForecastController> logger, NorthwindContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("Customers")]
        public string GetCustomers()
        {
            return _context.Employees.FirstOrDefault().FirstName;
        }
    }
}
