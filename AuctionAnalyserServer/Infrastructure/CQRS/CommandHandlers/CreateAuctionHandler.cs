using AuctionAnalyserServer.Base.CQRS.Command;
using AuctionAnalyserServer.Base.Interfaces.Services;
using AuctionAnalyserServer.Infrastructure.CQRS.Command;
using System;
using System.Threading.Tasks;

namespace AuctionAnalyserServer.Infrastructure.CQRS.CommandHandlers
{
    public class CreateAuctionHandler : ICommandHandlerAsync<AuctionCommand>
    {
        private readonly IAuctionService auctionService;

        public CreateAuctionHandler(IAuctionService auctionService)
        {
            this.auctionService = auctionService;
        }

        public Task HandleAsync(AuctionCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
