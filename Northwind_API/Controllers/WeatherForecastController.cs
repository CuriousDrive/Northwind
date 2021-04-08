using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetTenMostExpensiveProductsResult")]
        public async Task<IEnumerable<Northwind_API.Models.TenMostExpensiveProductsResult>> GetTenMostExpensiveProductsResult()
        {
            var proc = new Northwind_API.Data.NorthwindContextProcedures(new Data.NorthwindContext());
            return await proc.TenMostExpensiveProductsAsync();
        }
    }
}
