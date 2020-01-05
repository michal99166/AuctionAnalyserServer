using System;
using AuctionAnalyserServer.Infrastructure.DTO;

namespace AuctionAnalyserServer.Base.Interfaces.Services
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(Guid userId, string role);

    }
}