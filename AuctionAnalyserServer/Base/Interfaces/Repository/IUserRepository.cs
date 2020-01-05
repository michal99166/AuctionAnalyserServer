using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionAnalyserServer.Core.Domain.User;

namespace AuctionAnalyserServer.Base.Interfaces.Repository
{
    public interface IUserRepository : IRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task RemoveAsync(Guid id);
    }
}