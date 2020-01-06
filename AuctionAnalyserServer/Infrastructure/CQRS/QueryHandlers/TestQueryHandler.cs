using System;
using System.Threading.Tasks;
using AuctionAnalyserServer.Base.CQRS.Query;
using AuctionAnalyserServer.Infrastructure.CQRS.Command;
using AuctionAnalyserServer.Infrastructure.CQRS.QueryResult;

namespace AuctionAnalyserServer.Infrastructure.CQRS.QueryHandlers
{
    public class TestQueryHandler : IQueryHandler<TestQuery, TestQueryResult>
    {
        public TestQueryResult Execute(TestQuery query)
        {
            return new TestQueryResult("michal");
        }
    }
}