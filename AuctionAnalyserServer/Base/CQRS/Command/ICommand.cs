namespace AuctionAnalyserServer.Base.CQRS.Command
{
    public interface ICommand
    {
        string CurrentUserId { get; set; }
    }
}
