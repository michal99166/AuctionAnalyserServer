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
                List<HtmlDocument> documents = new List<HtmlDocument>();
                documents.Add(GetHtmlDocument(auction.Url));
                int pageNumber = GetPageNumber(documents.Single());

                if (pageNumber > 1)
                {
                    for (int i = 1; i < pageNumber; i++)
                    {
                        var document = GetHtmlDocument($"{auction.Url}&p={i}");
                        documents.Add(document);
                    }
                }

                //List<string> paragraphs = documents.DocumentNode.SelectNodes("//*[@class = 'b659611']")
                //        .Select(paragraphNode => paragraphNode.InnerHtml).ToList();

                //foreach (HtmlNode node in documents.DocumentNode.SelectNodes("//*[@class = 'b659611']"))
                //{
                //    //var allegro = node.G
                //    //Auction.AddResultToAction()
                //}
            }
        }

        private static int GetPageNumber(HtmlDocument document)
        {
            return int.Parse(document.DocumentNode.SelectSingleNode(
                "//*[@class='_1h7wt _1fkm6 _g1gnj _3db39_1hPb8 _3db39_12jAx']").InnerText);
        }

        private static HtmlDocument GetHtmlDocument(string url)
        {
            var website = $"{url}";
            var web = new HtmlWeb();
            var doc = web.Load(website);
            return doc;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}