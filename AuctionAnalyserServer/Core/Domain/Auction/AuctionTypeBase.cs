using AuctionAnalyserServer.Infrastructure.Exceptions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuctionAnalyserServer.Core.Domain.Auction
{
    public class AuctionTypeBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public int Id { get; protected set; }
        public AuctionType AuctionType { get; protected set; }
        public string Title { get; protected set; }
        public string Url { get; protected set; }
        public string Price { get; protected set; }

        public static AuctionTypeBase Create(string url, string title, string price)
            => new AuctionTypeBase(url, title, price);

        public AuctionTypeBase(string url, string title, string price)
        {
            Title = title;
            Price = price;
            SetAuctionType(url);
        }

        protected void SetAuctionType(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new DomainException(AuctionErrorCodes.InvalidAuctionUrl, "Pusty Url!");
            }

            Url = url;
            AuctionType = AuctionTypeResolver.GetAuctionType(url);
        }
    }
}