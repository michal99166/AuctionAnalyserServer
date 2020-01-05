using System.Threading.Tasks;

namespace AuctionAnalyserServer.Base.Interfaces
{
    public interface IDataInitializer : IService
    {
        Task SeedAsync();

    }
}