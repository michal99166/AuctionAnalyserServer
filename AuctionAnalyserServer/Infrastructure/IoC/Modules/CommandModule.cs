using AuctionAnalyserServer.Base.CQRS.Command;
using Autofac;
using System.Reflection;

namespace AuctionAnalyserServer.Infrastructure.IoC.Modules
{
    public class CommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(CommandModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                   .AsClosedTypesOf(typeof(ICommandHandler<>))
                   .InstancePerLifetimeScope();

            builder.RegisterType<CommandBus>()
                .As<ICommandBus>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
               .AsClosedTypesOf(typeof(ICommandHandlerAsync<>))
               .InstancePerLifetimeScope();

            builder.RegisterType<CommandBusAsync>()
                .As<ICommandBusAsync>()
                .InstancePerLifetimeScope();
        }
    }
}
