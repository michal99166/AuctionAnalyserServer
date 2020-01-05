using System.Threading.Tasks;
using AuctionAnalyserServer.Base.CQRS.Command;
using AuctionAnalyserServer.Base.CQRS.Query;

namespace AuctionAnalyserServer.Base.CQRS.Mediator
{
    public class CqrsMediator : ICqrsMediator
    {
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;
        private readonly IQueryBusAsync _queryBusAsync;
        private readonly ICommandBusAsync _commandBusAsync;

        public CqrsMediator(IQueryBus queryBus, ICommandBus commandBus, IQueryBusAsync queryBusAsync, ICommandBusAsync commandBusAsync)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
            _queryBusAsync = queryBusAsync;
            _commandBusAsync = commandBusAsync;
        }

        public void Execute<TCommand>(TCommand command) where TCommand : ICommand
        {
            _commandBus.ExecuteCommand<TCommand>(command);
        }

        public TResult Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            return _queryBus.ExecuteQuery<TQuery, TResult>(query);
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
