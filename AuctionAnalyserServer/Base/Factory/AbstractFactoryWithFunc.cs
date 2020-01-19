using System;
using System.Linq;
using AuctionAnalyserServer.Core.Domain.Auction;

namespace AuctionAnalyserServer.Base.Factory
{
    public abstract class AbstractFactory<TKey, TValue>
    {
        protected Func<TKey, TValue>[] Elements;

        protected virtual TValue GetElement(TKey key)
        {
            return Elements.Select(f => f(key)).FirstOrDefault(v => v != null);
        }
    }
}