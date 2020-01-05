using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionAnalyserServer.Base.Interfaces.Services;
using AuctionAnalyserServer.Infrastructure.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuctionAnalyserServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IJwtHandler _jwtHandler;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IJwtHandler jwtHandler)
        {
            _logger = logger;
            _jwtHandler = jwtHandler;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            Guid g = new Guid("90625146-a517-9d4c-ae70-26f4d7c493fd");

            var token = _jwtHandler.CreateToken(g, "user");

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
