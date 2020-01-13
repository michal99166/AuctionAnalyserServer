using AuctionAnalyserServer.Core.Domain.Auction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionAnalyserServer.Base.Interfaces.Repository
{
    public interface IAuctionRepository : IMongoRepository
    {
        Task<Auction> GetAsync(Guid id);
        Task<Auction> GetAsync(string url);
        Task<IEnumerable<Auction>> GetAllAsync();
        Task AddAsync(Auction auction);
        Task UpdateAsync(Auction auction);
        Task RemoveAsync(Guid id);
    }
}