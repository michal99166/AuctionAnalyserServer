namespace AuctionAnalyserServer.Infrastructure.CQRS.QueryResult
{
    public class TestQueryResult
    {
        public string Test { get; set; }

        public TestQueryResult(string test)
        {
            Test = test;
        }
    }
}