using AuctionAnalyserServer.Base.CQRS.Query;
using Autofac;
using System.Reflection;

namespace AuctionAnalyserServer.Infrastructure.IoC.Modules
{
    public class QueryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(QueryModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                   .AsClosedTypesOf(typeof(IQueryHandler<,>))
                   .InstancePerLifetimeScope();

            builder.RegisterType<QueryBus>()
                .As<IQueryBus>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
               .AsClosedTypesOf(typeof(IQueryHandlerAsync<,>))
               .InstancePerLifetimeScope();

            builder.RegisterType<QueryBusAsync>()
                .As<IQueryBusAsync>()
                .InstancePerLifetimeScope();
        }
    }
}
