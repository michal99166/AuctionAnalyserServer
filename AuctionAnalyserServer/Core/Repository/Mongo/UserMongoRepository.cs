using AuctionAnalyserServer.Base.Interfaces.Repository;
using AuctionAnalyserServer.Core.Domain.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionAnalyserServer.Base.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace AuctionAnalyserServer.Core.Repository.Mongo
{
    public class UserMongoRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;

        public UserMongoRepository (IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
            => await Model.AsQueryable().ToListAsync();

        public async Task AddAsync(User auction)
            => await Model.InsertOneAsync(auction);

        public async Task<User> GetAsync(string email)
            => await Model.AsQueryable().FirstOrDefaultAsync(x => x.Email == email);

        public async Task<User> GetAsync(Guid userId)
            => await Model.AsQueryable().FirstOrDefaultAsync(x => x.Id == userId);

        public async Task UpdateAsync(User user)
            => await Model.ReplaceOneAsync(x => x.Id == user.Id, user);

        public async Task RemoveAsync(Guid id)
            => await Model.DeleteOneAsync(x => x.Id == id);

        protected IMongoCollection<User> Model => _database.GetCollection<User>("Users");

    }
}