using AuctionAnalyserServer.Core.Domain.Auction;

namespace AuctionAnalyserServer.Core.Factory
{
    public class AuctionTypeResolver
    {
        public static AuctionType Allegro(string content)
        {
            return content.Contains(@"allegro") ? AuctionType.Allegro : AuctionType.None;
        }

        public static AuctionType Olx(string content)
        {
            return content.StartsWith("olx") ? AuctionType.Olx : AuctionType.None;
        }
    }
}