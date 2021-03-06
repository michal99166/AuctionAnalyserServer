﻿using System.Threading.Tasks;
using AuctionAnalyserServer.Base.CQRS.Command;
using AuctionAnalyserServer.Base.Interfaces.Services;
using AuctionAnalyserServer.Extensions;
using AuctionAnalyserServer.Infrastructure.CQRS.Command;
using Microsoft.Extensions.Caching.Memory;

namespace AuctionAnalyserServer.Infrastructure.CQRS.CommandHandlers
{
    public class LoginHandler : ICommandHandlerAsync<Login>
    {
        private readonly IUserService _userService;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMemoryCache _cache;

        public LoginHandler(IUserService userService, IJwtHandler jwtHandler,
            IMemoryCache cache)
        {
            _userService = userService;
            _jwtHandler = jwtHandler;
            _cache = cache;
        }

        public async Task HandleAsync(Login command)
        {
            await _userService.LoginAsync(command.Email, command.Password);
            var user = await _userService.GetAsync(command.Email);
            var jwt = _jwtHandler.CreateToken(user.Id, user.Role);
            _cache.SetJwt(command.TokenId, jwt);
        }

    }
}