using AuctionAnalyserServer.Infrastructure.CQRS.Command;
using System.Threading.Tasks;

namespace AuctionAnalyserServer.Base.Interfaces.Services
{
    public interface IAuctionService : IService
    {
        Task CreateAuctionAsync(AuctionCommand auctionCommand);
    }
}
