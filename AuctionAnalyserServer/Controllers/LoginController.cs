using AuctionAnalyserServer.Base.Controller;
using AuctionAnalyserServer.Base.CQRS.Mediator;
using AuctionAnalyserServer.Extensions;
using AuctionAnalyserServer.Infrastructure.CQRS.Command;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace AuctionAnalyserServer.Controllers
{
    public class LoginController : ApiControllerBase
    {
        private readonly IMemoryCache _cache;
        public LoginController(ICqrsMediator cqrsMediator, ICqrsMediatorAsync cqrsMediatorAsync, IMemoryCache cache) : base(cqrsMediator, cqrsMediatorAsync)
        {
            _cache = cache;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Login command)
        {
            command.TokenId = Guid.NewGuid();
            await ExecuteAsync(command);
            var jwt = _cache.GetJwt(command.TokenId);

            return Ok(jwt);
        }
    }
}