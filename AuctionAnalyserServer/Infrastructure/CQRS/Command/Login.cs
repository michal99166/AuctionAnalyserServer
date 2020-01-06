using System;
using AuctionAnalyserServer.Base.CQRS.Command;

namespace AuctionAnalyserServer.Infrastructure.CQRS.Command
{
    public class Login : ICommand
    {
        public Guid TokenId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}