using System;
using AuctionAnalyserServer.Core.Domain.Auction;

namespace AuctionAnalyserServer.Infrastructure.DTO
{
    public class AuctionDto
    {
        public Guid Id { get; set; }
        public string Name { get; protected set; }
        public string FilteredUrl { get; protected set; }
        public bool IsActive { get; protected set; }
        public AuctionType AuctionType { get; set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
    }
}