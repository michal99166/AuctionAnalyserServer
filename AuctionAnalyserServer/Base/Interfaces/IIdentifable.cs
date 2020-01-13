namespace AuctionAnalyserServer.Base.Interfaces
{
    public interface IIdentifable<TId>
    {
        TId Id { get; set; }
    }
}