using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionAnalyserServer.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string ClientUrl { get; set; }
        public int ExpiryMinutes { get; set; }
    }
}
