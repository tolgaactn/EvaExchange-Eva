using EvaExchange.Data;
using EvaExchange.Dto;
using EvaExchange.Entities;
using EvaExchange.Interfaces;
using System.Diagnostics;

namespace EvaExchange.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly EvaDbContext _context;

        public TradeRepository(EvaDbContext context)
        {
            _context = context;
        }

        public void Add(Trade trade)
        {
            _context.Add(trade);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Remove(id);
            _context.SaveChanges();
        }
        public void Delete(TradeDto tradeDto)
        {
            _context.Remove(tradeDto);
            _context.SaveChanges();
        }

        public List<Trade> GetAll()
        {
            var trades = _context.Trades.ToList();
            return trades;
        }

        public Trade GetById(int id)
        {
           
            return _context.Trades.Find(id);
        }

        public Trade GetByShareIdAndPortfolioId(int shareId, int portfolioId)
        {
            return _context.Trades
            .Where(t => t.ShareId == shareId && t.PortfolioId == portfolioId)
            .FirstOrDefault();
        }

        public int GetTotalShares(int portfolioId, int shareId)
        {
           

            var totalBuyQuantity = _context.Trades
       .Where(t => t.PortfolioId == portfolioId && t.ShareId == shareId && t.TradeType == TradeType.BUY)
       .Sum(t => t.TotalQuantity);

            var totalSellQuantity = _context.Trades
                .Where(t => t.PortfolioId == portfolioId && t.ShareId == shareId && t.TradeType == TradeType.SELL)
                .Sum(t => t.TotalQuantity);

            return totalBuyQuantity - totalSellQuantity;
        }

        

        public void Update(Trade trade)
        {
            _context.Trades.Update(trade);
            _context.SaveChanges();
        }
    }
}
