using System.Threading.Tasks;
using AuctionAnalyserServer.Base.CQRS.Command;
using AuctionAnalyserServer.Base.CQRS.Mediator;
using AuctionAnalyserServer.Base.CQRS.Query;

namespace AuctionAnalyserServer.Infrastructure.CQRS
{
    public class CqrsMediatorAsync : ICqrsMediatorAsync
    {
        private readonly IQueryBusAsync _queryBusAsync;
        private readonly ICommandBusAsync _commandBusAsync;

        public CqrsMediatorAsync(IQueryBusAsync queryBusAsync, ICommandBusAsync commandBusAsync)
        {
            _queryBusAsync = queryBusAsync;
            _commandBusAsync = commandBusAsync;
        }
        public async Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            await _commandBusAsync.ExecuteCommandAsync<TCommand>(command);
        }

        public async Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            return await _queryBusAsync.ExecuteQueryAsync<TQuery, TResult>(query);
        }
    }
}