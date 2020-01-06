using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionAnalyserServer.Base.Controller;
using AuctionAnalyserServer.Base.CQRS.Mediator;
using AuctionAnalyserServer.Infrastructure.CQRS.Command;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAnalyserServer.Controllers
{
    public class AuctionController : ApiControllerBase
    {
        public AuctionController(ICqrsMediator cqrsMediator, ICqrsMediatorAsync cqrsMediatorAsync) : base(cqrsMediator, cqrsMediatorAsync)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AuctionCommand command)
        {
            await ExecuteAsync(command);
            return Created($"users/{command.Email}", null);
        }
    }
}