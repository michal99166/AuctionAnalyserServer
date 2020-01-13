using System.Reflection;
using AuctionAnalyserServer.Workload;
using Autofac;
using Microsoft.Extensions.Hosting;

namespace AuctionAnalyserServer.Infrastructure.IoC.Modules
{
    public class WorkloadModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AllegroWorkload>()
                .As<IHostedService>()
                .InstancePerDependency();
        }
    }
}