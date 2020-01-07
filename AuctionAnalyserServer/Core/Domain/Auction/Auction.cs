using System;
using AuctionAnalyserServer.Infrastructure.Exceptions;

namespace AuctionAnalyserServer.Core.Domain.Auction
{
    public class Auction
    {
        public Guid AuctionId { get; protected set; }
        public string Name { get; protected set; }
        public string Url { get; protected set; }
        public bool IsActive { get; protected set; }
        public Guid UserId { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public Auction(string name, string url, bool isActive, Guid userId)
        {
            AuctionId = Guid.NewGuid();
            SetAuctionName(name);
            SetUrl(url);
            SetActive(isActive);
            SeUserId(userId);
            CreatedAt = DateTime.Now;
        }

        public void SetAuctionName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new DomainException(AuctionErrorCodes.InvalidAuctionName, "Auction name is invalid");
            }

            Name = name;
            UpdatedAt = DateTime.Now;
        }

        public void SetUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new DomainException(AuctionErrorCodes.InvalidAuctionUrl, "Auction name is invalid");
            }

            Url = url;
            UpdatedAt = DateTime.Now;
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
            UpdatedAt = DateTime.Now;
        }

        public void SeUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw  new DomainException(AuctionErrorCodes.InvalidUserId, "UserId is invalid");
            }

            UserId = userId;
            UpdatedAt = DateTime.Now;
        }
    }
}