using System;
using System.Threading.Tasks;
using AuctionAnalyserServer.Base.CQRS.Command;
using AuctionAnalyserServer.Base.CQRS.Mediator;
using AuctionAnalyserServer.Base.CQRS.Query;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAnalyserServer.Base.Controller
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private readonly ICqrsMediator _cqrsMediator;
        private readonly ICqrsMediatorAsync _cqrsMediatorAsync;

        protected Guid UserId => User?.Identity?.IsAuthenticated == true ?
            Guid.Parse(User.Identity.Name) :
            Guid.Empty;

        protected ApiControllerBase(ICqrsMediator cqrsMediator, ICqrsMediatorAsync cqrsMediatorAsync)
        {
            _cqrsMediator = cqrsMediator;
            _cqrsMediatorAsync = cqrsMediatorAsync;
        }

        protected void Execute<T>(T command) where T : ICommand
        {
            SetUserIdCommand(command);
            _cqrsMediator.Execute(command);
        }

        protected TResult Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            SetUserIdQuery(query);
            return  _cqrsMediator.Execute<TQuery, TResult>(query);
        }

        protected async Task ExecuteAsync<T>(T command) where T : ICommand
        {
            SetUserIdCommand(command);
            await _cqrsMediatorAsync.ExecuteAsync(command);
        }

        protected async Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            SetUserIdQuery(query);
            return await _cqrsMediatorAsync.ExecuteAsync<TQuery, TResult>(query);
        }

        #region Private methods
        private void SetUserIdCommand<T>(T command) where T : ICommand
        {
            if (command is IAuthenticatedCommand authenticatedCommand)
            {
                authenticatedCommand.UserId = UserId;
            }
        }

        private void SetUserIdQuery<T>(T query) where T : IQuery
        {
            if (query is IAuthenticatedQuery authenticatedQuery)
            {
                authenticatedQuery.UserId = UserId;
            }
        }
        #endregion
    }
}