using System;
using AuctionAnalyserServer.Base.Interfaces;
using AuctionAnalyserServer.Core.Factory;
using AuctionAnalyserServer.Infrastructure.Exceptions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuctionAnalyserServer.Core.Domain.Auction
{
    public class AuctionDetails : IIdentifable<int>
    {
        [BsonId]
        public int Id { get; set; }
        public string Title { get; protected set; }
        public string AuctionUrl { get; protected set; }
        public string Price { get; protected set; }
        public string Currency { get; protected set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int UpdateCount { get; set; }
        public static AuctionDetails Create(string url, string title, string price, string currency)
            => new AuctionDetails(url, title, price, currency);

        private AuctionDetails(string url, string title, string price, string currency)
        {
            SetTitle(title);
            SetPrice(price);
            SetAuctionUrl(url);
            SetCurrency(currency);
            CreatedTime = DateTime.Now;
            UpdateCount = 0;
        }

        public void SetCurrency(string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
            {
                throw new AuctionDetailsException(AuctionErrorCodes.InvalidCurrency, "Currency can not be empty.");
            }

            Currency = currency;
            UpdateTime = DateTime.Now;
        }

        public void SetTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new AuctionDetailsException(AuctionErrorCodes.InvalidTitle, "Title can not be empty.");
            }

            Title = title;
            UpdateTime = DateTime.Now;
        }
        public void SetPrice(string price)
        {
            if (string.IsNullOrEmpty(price))
            {
                throw new AuctionDetailsException(AuctionErrorCodes.InvalidPrice, "Price can not be empty.");
            }

            if (!double.TryParse(price, out double result))
            {
                throw new AuctionDetailsException(AuctionErrorCodes.InvalidPriceFormat, "Invalid price format.");
            }
            Price = price;
            UpdateTime = DateTime.Now;
        }

        public void SetAuctionUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new AuctionDetailsException(AuctionErrorCodes.InvalidAuctionUrl, "FilteredUrl can not be empty.!");
            }

            AuctionUrl = url;
            UpdateTime = DateTime.Now;
        }

    }
}