using System;
using System.Linq;
using AuctionAnalyserServer.Base.Factory;
using AuctionAnalyserServer.Core.Domain.Auction;

namespace AuctionAnalyserServer.Core.Factory
{
    public class AuctionTypeFactory : AbstractFactory<string, AuctionType>
    {
        private AuctionTypeFactory()
        {
            Elements = new Func<string, AuctionType>[]
            {
                AuctionTypeResolver.Olx,
                AuctionTypeResolver.Allegro
            };
        }

        public static AuctionTypeFactory CreateFactory()
        {
            return new AuctionTypeFactory();
        }

        protected override AuctionType GetElement(string key)
        {
            return Elements.Select(f => f(key.ToLower())).FirstOrDefault(v => v != AuctionType.None);
        }

        public AuctionType GetAuctionType(string url)
        {
            return GetElement(url);
        }
    }
}