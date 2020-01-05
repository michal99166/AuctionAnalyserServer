using AuctionAnalyserServer.Infrastructure.IoC.Modules;
using AuctionAnalyserServer.Infrastructure.Mappers;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace AuctionAnalyserServer.Infrastructure.IoC
{
    public class ContainerModule : Autofac.Module
    {

        private readonly IConfiguration _configuration;

        public ContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutomapperConfig.Initialize())
                .SingleInstance();
            builder.RegisterModule<CommandModule>();
            builder.RegisterModule<QueryModule>();
            builder.RegisterModule<CqrsMediatorModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<SqlModule>();
            builder.RegisterModule<MongoModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule(new SettingsModule(_configuration));
        }
    }
}
