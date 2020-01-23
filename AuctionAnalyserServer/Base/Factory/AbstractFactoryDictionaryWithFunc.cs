using System;
using System.Collections.Generic;
using System.Linq;

namespace AuctionAnalyserServer.Base.Factory
{
    public abstract class AbstractFactoryDictionaryWithFunc<TKey, TValue>
    {
        protected IDictionary<TKey, Func<TValue>> FactoryItems;

        public virtual TValue GetItem(TKey key)
        {
            return FactoryItems.ContainsKey(key) ? FactoryItems[key]() : default(TValue);
        }
    }

    public abstract class GenericFactoryWithFunc<TKey, TValue, TValueResult>
    {
        protected IDictionary<TKey, Func<TValue, TValueResult>> FactoryItems;

        public virtual TValueResult GetItem(TKey key, TValue value)
        {
            return FactoryItems.ContainsKey(key) ? FactoryItems[key](value) : default(TValueResult);
        }
    }

    public abstract class GenericFactoryWithFunc<TKey, TValue1, TValue2, TValueResult>
    {
        protected IDictionary<TKey, Func<TValue1, TValue2, TValueResult>> FactoryItems;

        public virtual TValueResult GetItem(TKey key, TValue1 value1, TValue2 value2)
        {
            return FactoryItems.ContainsKey(key) ? FactoryItems[key](value1, value2) : default(TValueResult);
        }
    }
}