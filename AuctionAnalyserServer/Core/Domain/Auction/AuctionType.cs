using System;
using System.Collections.Generic;
using System.Linq;

namespace AuctionAnalyserServer.Core.Domain.Auction
{
    public enum AuctionType
    {
        None,
        Allegro,
        Olx,
        AliExpress
    }

    public static class AuctionTypeResolver
    {
        private static readonly ISet<Func<string, AuctionType>> _array = new HashSet<Func<string, AuctionType>>
        {
            url => url.Contains("allegro") ? AuctionType.Allegro : AuctionType.None,
            url => url.Contains("olx") ? AuctionType.Olx : AuctionType.None,
            url => url.Contains("aliexpress") ? AuctionType.AliExpress : AuctionType.None
        };

        public static AuctionType GetAuctionType(string url)
        {
            Uri uri = new Uri(url);
            string hostName = uri.Host.ToLower();
            var result = _array.FirstOrDefault(x => x.Invoke(hostName) != AuctionType.None);
            return result(hostName);
        }
    }
}