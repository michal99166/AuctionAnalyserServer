using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionAnalyserServer.Base.Interfaces;
using AuctionAnalyserServer.Core.Domain.Auction;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace AuctionAnalyserServer.Core.Repository.Mongo
{
    public class AuctionMongoRepository : IMongoRepository
    {
        private readonly IMongoDatabase _database;

        public AuctionMongoRepository(IMongoDatabase database)
        {
            _database = database;
        }

        //public async Task<Auction> GetAsync(string email)
        //    => await Auction.AsQueryable().FirstOrDefaultAsync(x => x.)

        //public async Task<Auction> GetAsync(Guid userId)
        //    => await Auction.AsQueryable().FirstOrDefaultAsync(x => x.Id == userId);

        public async Task<IEnumerable<Auction>> GetAllAsync()
            => await Auction.AsQueryable().ToListAsync();

        //public async Task AddAsync(Auction user)
        //    => await Auction.InsertOneAsync(user);

        //public async Task UpdateAsync(Auction user)
        //    => await Auction.ReplaceOneAsync(x => x.Id == user.Id, user);

        //public async Task RemoveAsync(Guid id)
        //    => await Auction.DeleteOneAsync(x => x.Id == id);

        private IMongoCollection<Auction> Auction => _database.GetCollection<Auction>("Auction");
    }
}