using AuctionAnalyserServer.Base.CQRS.Mediator;
using AuctionAnalyserServer.Infrastructure.CQRS;
using Autofac;

namespace AuctionAnalyserServer.Infrastructure.IoC.Modules
{
    public class CqrsMediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CqrsMediator>()
                .As<ICqrsMediator>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CqrsMediatorAsync>()
                .As<ICqrsMediatorAsync>()
                .InstancePerLifetimeScope();
        }
    }
}
