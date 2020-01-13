using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionAnalyserServer.Base.Interfaces;
using AuctionAnalyserServer.Base.Interfaces.Repository;
using AuctionAnalyserServer.Core.Domain.Auction;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace AuctionAnalyserServer.Core.Repository.Mongo
{
    public class AuctionMongoRepository : IAuctionRepository
    {
        private readonly IMongoDatabase _database;

        public AuctionMongoRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Auction>> GetAllAsync()
            => await Model.AsQueryable().ToListAsync();

        public async Task AddAsync(Auction auction)
            => await Model.InsertOneAsync(auction);

        public async Task<Auction> GetAsync(string url)
            => await Model.AsQueryable().FirstOrDefaultAsync(x => x.Url == url);

        public async Task<Auction> GetAsync(Guid userId)
            => await Model.AsQueryable().FirstOrDefaultAsync(x => x.UserId == userId);

        public async Task UpdateAsync(Auction auction)
            => await Model.ReplaceOneAsync(x => x.Id == auction.Id, auction);

        public async Task RemoveAsync(Guid id)
            => await Model.DeleteOneAsync(x => x.Id == id);

        protected IMongoCollection<Auction> Model => _database.GetCollection<Auction>("Auctions");
    }
}