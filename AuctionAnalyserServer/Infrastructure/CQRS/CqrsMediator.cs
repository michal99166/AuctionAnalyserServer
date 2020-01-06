using System.Threading.Tasks;
using AuctionAnalyserServer.Base.CQRS.Command;
using AuctionAnalyserServer.Base.CQRS.Mediator;
using AuctionAnalyserServer.Base.CQRS.Query;

namespace AuctionAnalyserServer.Infrastructure.CQRS
{
    public class CqrsMediator : ICqrsMediator
    {
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;

        public CqrsMediator(IQueryBus queryBus, ICommandBus commandBus)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
        }

        public void Execute<TCommand>(TCommand command) where TCommand : ICommand
        {
            _commandBus.ExecuteCommand<TCommand>(command);
        }

        public TResult Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            return _queryBus.ExecuteQuery<TQuery, TResult>(query);
        }
    }
}
