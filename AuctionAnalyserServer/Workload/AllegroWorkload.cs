using System;
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
        private int AuctionCounter = 0;

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
                if (!auctions.Any())
                {
                    return;
                }
                ProcessAuctions(auctions, new List<HtmlDocument>());
                await Task.Delay(10000, cancellationToken);

            } while (!cancellationToken.IsCancellationRequested);
        }

        private async void ProcessAuctions(IEnumerable<AuctionDto> autcionsUrl, List<HtmlDocument> htmlPages)
        {
            foreach (var auction in autcionsUrl)
            {
                List<AuctionDetails> auctions = new List<AuctionDetails>();
                htmlPages.Add(GetHtmlDocument(auction.FilteredUrl));
                GetHtmlPages(auction, htmlPages);
                GetAuctionsFromPage(htmlPages, auctions);
                await _auctionService.AddAuctionDetailsAsync(auction.FilteredUrl, auctions);
            }
        }

        private void GetAuctionsFromPage(List<HtmlDocument> documents, List<AuctionDetails> auctionDetails)
        {
            foreach (var document in documents)
            {
                AuctionDetails auctionDetailTemp;
                try
                {
                    auctionDetailTemp = AuctionDetails.Create(
                        document.DocumentNode.SelectNodes("//h2[@class='ebc9be2']//a").First().ChildNodes[0]?.InnerText,
                        document.DocumentNode.SelectNodes("//h2[@class='ebc9be2']//a").First().Attributes["href"]?.Value,
                        document.DocumentNode.SelectNodes("//span[@class='fee8042']").First().ChildNodes[0]?.InnerText,
                        document.DocumentNode.SelectNodes("//span[@class='fee8042']").First().ChildNodes[3]?.InnerText);
                     auctionDetails.Add(auctionDetailTemp);
                }
                catch (Exception ex)
                {
                    AuctionCounter--;
                    _logger.LogCritical(ex, "Error while downloading auction.");
                    continue;
                }

                AuctionCounter++;
            }

            
            _logger.LogInformation($"Downloaded progress auction : {AuctionCounter} / {documents.Count}");
        }

        private void GetHtmlPages(AuctionDto auction, List<HtmlDocument> documents)
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