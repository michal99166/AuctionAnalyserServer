using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionAnalyserServer.Base.Interfaces;
using AuctionAnalyserServer.Base.Interfaces.Services;
using log4net;
using log4net.Repository.Hierarchy;

namespace AuctionAnalyserServer.Core.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;

        public DataInitializer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task SeedAsync()
        {
            try
            {
                var users = await _userService.BrowseAsync();
                if (users.Any())
                {
                    return;
                }
                var tasks = new List<Task>();
                for (var i = 1; i <= 10; i++)
                {
                    var userId = Guid.NewGuid();
                    var username = $"user{i}";
                    await _userService.RegisterAsync(userId, $"user{i}@test.com", username, "secret", "user");
                }
                for (var i = 1; i <= 3; i++)
                {
                    var userId = Guid.NewGuid();
                    var username = $"admin{i}";
                    await _userService.RegisterAsync(userId, $"admin{i}@test.com",
                        username, "secret", "admin");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}