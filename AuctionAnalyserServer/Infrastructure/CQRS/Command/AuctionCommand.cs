using AuctionAnalyserServer.Base.CQRS.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionAnalyserServer.Infrastructure.CQRS.Command
{
    public class AuctionCommand : AuthenticatedCommandBase
    {
        public string AuctionName { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public string Comment { get; set; }
    }
}