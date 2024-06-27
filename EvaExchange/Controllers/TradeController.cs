using EvaExchange.Dto;
using EvaExchange.Entities;
using EvaExchange.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EvaExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        private readonly ITradeRepository _tradeRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IShareRepository _shareRepository;
        private readonly IPortfolioShareRepository _portfolioShareRepository;


        public TradeController(ITradeRepository tradeRepository, IPortfolioRepository portfolioRepository, IShareRepository shareRepository, IPortfolioShareRepository portfolioShareRepository)
        {
            _tradeRepository = tradeRepository;
            _portfolioRepository = portfolioRepository;
            _shareRepository = shareRepository;
            _portfolioShareRepository = portfolioShareRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Trade>>> GetAll()
        {
            return _tradeRepository.GetAll();
        }

        [HttpPost("Buy")]
        public async Task<ActionResult> Buy(TradeDto tradeDto)
        {
            var share = _shareRepository.GetById(tradeDto.ShareId);
            var portfolio = _portfolioRepository.GetById(tradeDto.PortfolioId);
            var portfolioShare = _portfolioShareRepository.GetByShareIdAndPortfolioId(tradeDto.ShareId,tradeDto.PortfolioId);
            if (share == null)
            {
                return BadRequest("Share not found");
            }
            if (portfolio == null)
            {
                return BadRequest("Portfolio not found");
            }
            if ((tradeDto.TotalQuantity * share.Price) > portfolio.TotalAmount)
            {
                return BadRequest("Portfolio amount not enough for this trade");
            }
            if (share.Quantity < tradeDto.TotalQuantity || share.Quantity == 0)
            {
                return BadRequest("Share quantity not enough for this trade for BUY");
            }


            
            var trade = new Trade
            {
                PortfolioId = tradeDto.PortfolioId,
                ShareId = share.Id,
                TotalQuantity = tradeDto.TotalQuantity,
                TradeType = TradeType.BUY,
                TradeTime = DateTime.UtcNow,
                TotalPrice = (tradeDto.TotalQuantity * share.Price),
            };
            _tradeRepository.Add(trade);
            




            portfolio.TotalAmount -= tradeDto.TotalQuantity * share.Price;
            share.Quantity -= tradeDto.TotalQuantity;




            if (portfolioShare != null)
            {
                portfolioShare.ShareQuantity += tradeDto.TotalQuantity;
                _portfolioShareRepository.Update(portfolioShare);

            }
            else
            {


                var newPortfolioShare = new PortfolioShare
                {
                    PortfolioId = tradeDto.PortfolioId,  
                    ShareId = tradeDto.ShareId,
                    ShareQuantity = tradeDto.TotalQuantity,
                };
                _portfolioShareRepository.Add(newPortfolioShare);
            }


            _portfolioRepository.Update(portfolio);
            _shareRepository.Update(share);


            return Ok();



        }
        [HttpPost("Sell")]
        public async Task<ActionResult> Sell(TradeDto tradeDto)
        {
            var share = _shareRepository.GetById(tradeDto.ShareId);
            var portfolio = _portfolioRepository.GetById(tradeDto.PortfolioId);
            var totalOwnedShares = _tradeRepository.GetTotalShares(tradeDto.PortfolioId, tradeDto.ShareId);
            var portfolioShare = _portfolioShareRepository.GetByShareIdAndPortfolioId(tradeDto.ShareId, tradeDto.PortfolioId);

            if (share == null)
            {
                return BadRequest("Share not found");
            }
            if (portfolio == null)
            {
                return BadRequest("Portfolio not found");
            }
            if (totalOwnedShares < tradeDto.TotalQuantity)
            {
                return BadRequest("Share quantity not enough for this trade");
            }


            
            var trade = new Trade
            {
                PortfolioId = tradeDto.PortfolioId,
                ShareId = share.Id,
                TotalQuantity = tradeDto.TotalQuantity,
                TradeTime = DateTime.UtcNow,
                TradeType = TradeType.SELL,
                TotalPrice = (tradeDto.TotalQuantity * share.Price),
            };
            portfolioShare.ShareQuantity -= tradeDto.TotalQuantity;

            _portfolioShareRepository.Update(portfolioShare);
            _tradeRepository.Add(trade);



            portfolio.TotalAmount += tradeDto.TotalQuantity * share.Price;
            share.Quantity += tradeDto.TotalQuantity;


            _portfolioRepository.Update(portfolio);
            _shareRepository.Update(share);

            
            return Ok();



        }
        
    }
}