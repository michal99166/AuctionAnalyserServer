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
    public class UserMongoRepository : IUserRepository, IMongoRepository
    {
        private readonly IMongoDatabase _database;

        public UserMongoRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<User> GetAsync(string email)
            => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Email == email);

        public async Task<User> GetAsync(Guid userId)
            => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Id == userId);

        public async Task<IEnumerable<User>> GetAllAsync()
            => await Users.AsQueryable().ToListAsync();

        public async Task AddAsync(User user)
            => await Users.InsertOneAsync(user);

        public async Task UpdateAsync(User user)
            => await Users.ReplaceOneAsync(x => x.Id == user.Id, user);

        public async Task RemoveAsync(Guid id)
            => await Users.DeleteOneAsync(x => x.Id == id);

        private IMongoCollection<User> Users => _database.GetCollection<User>("Users");


    }
}