using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using AuctionAnalyserServer.Base.Interfaces.Services;
using AuctionAnalyserServer.Core.Domain.Auction;
using AuctionAnalyserServer.Infrastructure.DTO;
using HtmlAgilityPack;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AuctionAnalyserServer.Workload
{
    public class AuctionGatherBaseHostedService : IHostedService
    {
        private readonly ILogger<AuctionGatherBaseHostedService> _logger;
        private readonly IAuctionService _auctionService;
        private int AuctionCounter = 0;

        public AuctionGatherBaseHostedService(ILogger<AuctionGatherBaseHostedService> logger, IAuctionService auctionService)
        {
            _logger = logger;
            _auctionService = auctionService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            do
            {
                var auctions = await _auctionService.GetAsync();
                _logger.LogInformation($"Znaleziono {auctions.Count()} aukcji do przetworzenia.");
                if (!auctions.Any())
                {
                    return;
                }
                ProcessAuctions(auctions);
                await Task.Delay(10000, cancellationToken);

            } while (!cancellationToken.IsCancellationRequested);
        }

        private async void ProcessAuctions(IEnumerable<AuctionDto> auctions)
        {
            foreach (var auction in auctions)
            {
                var auctionDetails = new List<AuctionDetails>();
                var htmlPages = new List<HtmlDocument>

                {
                    GetHtmlDocument(auction.FilteredUrl)
                };

                GetHtmlDocumentPages(auction, htmlPages);
                GetAuctionsDetailsFromPage(htmlPages, auctionDetails);
                await _auctionService.AddAuctionDetailsAsync(auction.FilteredUrl, auctionDetails);
            }
        }

        private void GetAuctionsDetailsFromPage(List<HtmlDocument> pages, List<AuctionDetails> auctionDetails)
        {
            foreach (var page in pages)
            {
                try
                {
                    foreach (var auction in page.DocumentNode.SelectNodes(".//*[@class='_00d6b80']"))
                    {
                        string title = auction.SelectSingleNode(".//*[@class='_734e7e0']").FirstChild.ChildNodes[0].InnerText;
                        string url = auction.SelectSingleNode(".//*[@class='_734e7e0']").FirstChild.FirstChild.GetAttributeValue("href", "title");
                        string price = auction.SelectSingleNode(".//*[@class='fee8042']").ChildNodes[0].InnerText.Replace(" ", string.Empty);
                        string currency = auction.SelectSingleNode(".//span[@class='_31f32cc']")?.InnerText;
                        auctionDetails.Add(AuctionDetails.Create(url, title, price, currency));
                    }
                }
                catch (Exception ex)
                {
                    AuctionCounter--;
                    _logger.LogCritical(ex, "Error while downloading auction.");
                    continue;
                }

                AuctionCounter++;
            }

            _logger.LogInformation($"Downloaded progress auction : {AuctionCounter} / {pages.Count}");
        }

        private void GetHtmlDocumentPages(AuctionDto auction, List<HtmlDocument> documents)
        {
            int pageCount = GetPageNumber(documents.Single());
            _logger.LogInformation($"Znaleziono stron: {pageCount} dla aukcji: {auction.FilteredUrl}");
            if (pageCount > 1)
            {
                for (int i = 1; i < pageCount; i++)
                {
                    var document = GetHtmlDocument($"{auction.FilteredUrl}&p={i}");
                    documents.Add(document);
                }
            }
        }

        private int GetPageNumber(HtmlDocument document)
        {
            return int.Parse(document.DocumentNode.SelectSingleNode(
                "//*[@class='_1h7wt _1fkm6 _g1gnj _3db39_1hPb8 _3db39_12jAx']").InnerText);
        }

        private HtmlDocument GetHtmlDocument(string url)
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