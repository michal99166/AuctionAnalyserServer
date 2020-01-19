using System;
using System.Collections.Generic;
using System.Linq;
using AuctionAnalyserServer.Base.Interfaces;
using AuctionAnalyserServer.Core.Factory;
using AuctionAnalyserServer.Infrastructure.Exceptions;

namespace AuctionAnalyserServer.Core.Domain.Auction
{
    public class Auction : IIdentifable<Guid>
    {
        private ISet<AuctionDetails> _auctionDetail = new HashSet<AuctionDetails>();
        public Guid Id { get; set; }
        public string Name { get; protected set; }
        public string FilteredUrl { get; protected set; }
        public bool IsActive { get; protected set; }
        public IEnumerable<AuctionDetails> AuctionDetail
        {
            get => _auctionDetail;
            set => _auctionDetail = new HashSet<AuctionDetails>(value);
        }
        public AuctionType AuctionType { get; protected set; }
        public Guid UserId { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public Auction()
        {
            
        }
        private Auction(string name, string url, bool isActive, Guid userId)
        {
            Id = Guid.NewGuid();
            SetAuctionName(name);
            SetFilteredUrl(url);
            SetActive(isActive);
            SetOwnerUserId(userId);
            CreatedAt = DateTime.Now;
        }

        public static Auction Create(string name, string url, bool isActive, Guid userId)
        {
            return new Auction(name, url, isActive, userId);
        }

        public void AddAuctionDetails(AuctionDetails auctionDetails)
        {
            if (auctionDetails is null)
            {
                throw new DomainException(AuctionErrorCodes.InvalidAuctionDetails, "Any auction details to add.");
            }

            _auctionDetail.Add(AuctionDetails.Create(auctionDetails.AuctionUrl, auctionDetails.Title, auctionDetails.Price, auctionDetails.Currency));
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

        public void SetFilteredUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new DomainException(AuctionErrorCodes.InvalidAuctionUrl, "Auction name is invalid");
            }

            FilteredUrl = url.ToLower();
            AuctionType = AuctionTypeFactory.CreateFactory().GetAuctionType(url);
            UpdatedAt = DateTime.Now;
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
            UpdatedAt = DateTime.Now;
        }

        public void SetOwnerUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new DomainException(AuctionErrorCodes.InvalidUserId, "UserId is invalid");
            }

            UserId = userId;
            UpdatedAt = DateTime.Now;
        }
    }
}