using AuctionAnalyserServer.Base.Controller;
using AuctionAnalyserServer.Base.CQRS.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAnalyserServer.Controllers
{
    public class AuthController : ApiControllerBase
    {
        public AuthController(ICqrsMediator dataBus) : base(dataBus)
        {
        }

        public IActionResult Index()
        {
            return NoContent();
        }
    }
}