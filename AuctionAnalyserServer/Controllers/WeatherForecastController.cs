using AuctionAnalyserServer.Base.Controller;
using AuctionAnalyserServer.Base.CQRS.Mediator;
using AuctionAnalyserServer.Infrastructure.CQRS.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionAnalyserServer.Infrastructure.CQRS.QueryResult;

namespace AuctionAnalyserServer.Controllers
{
    public class WeatherForecastController : ApiControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        public WeatherForecastController(ICqrsMediator cqrsMediator, ICqrsMediatorAsync cqrsMediatorAsync) : base(cqrsMediator, cqrsMediatorAsync)
        {
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Authorize]
        [HttpGet("Test")]
        public IActionResult GetTest([FromBody] TestQuery testQuery)
        {
            var result = Execute<TestQuery, TestQueryResult>(testQuery);
            return Ok(result);
        }
    }
}
