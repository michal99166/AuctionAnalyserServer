﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionAnalyserServer.Base.CQRS.Query
{
    public interface IQueryBus
    {
        TResult ExecuteQuery<TQuery, TResult>(TQuery query) where TQuery : IQuery;
    }
}
