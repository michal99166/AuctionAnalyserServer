namespace AuctionAnalyserServer.Core.Domain.Auction
{
    public class AuctionErrorCodes
    {
        public static string InvalidAuctionName => "invalid_auction_name";
        public static string InvalidAuctionUrl => "invalid_auction_url";
        public static string InvalidTitle => "invalid_title";
        public static string InvalidPrice => "invalid_price";
        public static string InvalidPriceFormat => "invalid_price_format";
        public static string InvalidUserId => "invalid_auction_userid";
        public static string InvalidCurrency => "invalid_currency_empty";
        public static string InvalidAuctionDetails => "invalid_auction_DetailsEmpty_empty";

    }
}