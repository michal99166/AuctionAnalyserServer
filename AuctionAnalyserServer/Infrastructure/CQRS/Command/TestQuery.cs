using AuctionAnalyserServer.Base.CQRS.Command;
using AuctionAnalyserServer.Base.CQRS.Query;

namespace AuctionAnalyserServer.Infrastructure.CQRS.Command
{
    public class TestQuery : AuthenticatedQueryBase
    {
        public string Name { get; set; }
    }
}