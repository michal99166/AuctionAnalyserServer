using AuctionAnalyserServer.Base.Controller;
using AuctionAnalyserServer.Base.CQRS.Mediator;
using AuctionAnalyserServer.Base.Interfaces.Services;
using AuctionAnalyserServer.Infrastructure.CQRS.Command;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuctionAnalyserServer.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;
        public UserController(ICqrsMediator cqrsMediator, ICqrsMediatorAsync cqrsMediatorAsync, IUserService userService) : base(cqrsMediator, cqrsMediatorAsync)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAsync();

            return Ok(users);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var user = await _userService.GetAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateUser command)
        {
            await ExecuteAsync(command);
            return Created($"users/{command.Email}", null);
        }
    }
}