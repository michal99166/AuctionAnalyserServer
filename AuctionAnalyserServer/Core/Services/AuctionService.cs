using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionAnalyserServer.Base.Interfaces.Repository;
using AuctionAnalyserServer.Base.Interfaces.Services;
using AuctionAnalyserServer.Core.Domain.Auction;
using AuctionAnalyserServer.Infrastructure.CQRS.Command;
using AuctionAnalyserServer.Infrastructure.DTO;
using AuctionAnalyserServer.Infrastructure.Exceptions;
using AutoMapper;

namespace AuctionAnalyserServer.Core.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly IAuctionRepository _repository;
        private readonly IMapper _mapper;

        public AuctionService(IAuctionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task CreateAuctionAsync(AuctionCommand auctionCommand, Guid userId)
        {
            var auction = await _repository.GetAsync(auctionCommand.Url);
            if (auction != null)
            {
                throw new Exception("Auction already existing in database.");
            }

            Auction auctionNew = Auction.Create(auctionCommand.AuctionName, auctionCommand.Url, auctionCommand.IsActive, userId);
            await _repository.AddAsync(auctionNew);
        }

        public async Task<AuctionDto> GetAsync(string auctionName)
        {
            var auction = await _repository.GetAsync(auctionName);

            return _mapper.Map<Auction, AuctionDto>(auction);
        }

        public async Task<IEnumerable<AuctionDto>> GetAsync()
        {
            var auctions = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<Auction>, IEnumerable<AuctionDto>>(auctions);
        }

        public async Task UpdateAuctionAsync(Auction auction)
        {
            var auctionDb = await _repository.GetAsync(auction.FilteredUrl);
            if (auctionDb is null)
            {
                throw new Exception("Auction does not exist in database.");
            }

            await _repository.UpdateAsync(auctionDb);
        }

        public async Task AddAuctionDetailsAsync(string url, IEnumerable<AuctionDetails> auctionDetails)
        {
            var auctionDb = await _repository.GetAsync(url);
            if (auctionDb is null)
            {
                throw new Exception("Auction does not exist in database.");
            }
            
            foreach (var auctionDetail in auctionDetails)
            {
                auctionDb.AddAuctionDetails(auctionDetail);
            }
            await _repository.UpdateAsync(auctionDb);

        }
    }
}