using System.Collections.Generic;
using AuctionAnalyserServer.Core.Domain.Auction;
using AuctionAnalyserServer.Core.Domain.User;
using AuctionAnalyserServer.Infrastructure.DTO;
using AutoMapper;

namespace AuctionAnalyserServer.Infrastructure.Mappers
{
    public static class AutomapperConfig
    {
        public static IMapper Initialize()
                => new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<User, UserDto>();
                        cfg.CreateMap<Auction, AuctionDto>();
                    }).CreateMapper();
    }
}