using System;
using System.Threading.Tasks;
using AuctionAnalyserServer.Base.CQRS.Command;
using AuctionAnalyserServer.Base.Interfaces.Services;
using AuctionAnalyserServer.Infrastructure.CQRS.Command;

namespace AuctionAnalyserServer.Infrastructure.CQRS.CommandHandlers
{
    public class CreateUserHandler : ICommandHandlerAsync<CreateUser>
    {
        private readonly IUserService _userService;

        public CreateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(CreateUser command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), command.Email,
                command.Username, command.Password, command.Role);
        }
    }
}