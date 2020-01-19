using System;
using System.Collections.Generic;
using AuctionAnalyserServer.Infrastructure.CQRS.Command;
using System.Threading.Tasks;
using AuctionAnalyserServer.Core.Domain.Auction;
using AuctionAnalyserServer.Infrastructure.DTO;

namespace AuctionAnalyserServer.Base.Interfaces.Services
{
    public interface IAuctionService : IService
    {
        Task CreateAuctionAsync(AuctionCommand auctionCommand, Guid userId);
        Task<AuctionDto> GetAsync(string auctionName);
        Task<IEnumerable<AuctionDto>> GetAsync();
        Task UpdateAuctionAsync(Auction auction);
        Task AddAuctionDetailsAsync(string url, IEnumerable<AuctionDetails> auctionDetails);

    }
}
