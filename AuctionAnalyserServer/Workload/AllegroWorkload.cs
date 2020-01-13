using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuctionAnalyserServer.Base.Interfaces.Services;
using AuctionAnalyserServer.Core.Domain.Auction;
using AuctionAnalyserServer.Infrastructure.DTO;
using HtmlAgilityPack;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AuctionAnalyserServer.Workload
{
    public class AllegroWorkload : IHostedService
    {
        private readonly ILogger<AllegroWorkload> _logger;
        private readonly IAuctionService _auctionService;

        public AllegroWorkload(ILogger<AllegroWorkload> logger, IAuctionService auctionService)
        {
            _logger = logger;
            _auctionService = auctionService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            do
            {
                var auctions = await _auctionService.GetAsync();
                _logger.LogInformation($"Znaleziono aukcji allegro {auctions.Count()} do przetworzenia.");

                ProcessAuctions(auctions);
                await Task.Delay(10000, cancellationToken);

            } while (!cancellationToken.IsCancellationRequested);
        }

        private static void ProcessAuctions(IEnumerable<AuctionDto> auctions)
        {
            foreach (var auction in auctions)
            {
                var doc = GetHtmlDocument(auction);
                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//a"))
                {
                    //var allegro = node.G
                    //Auction.AddResultToAction()
                }
            }
        }

        private static HtmlDocument GetHtmlDocument(AuctionDto auction)
        {
            var url = $"{auction.Url}";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            return doc;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}