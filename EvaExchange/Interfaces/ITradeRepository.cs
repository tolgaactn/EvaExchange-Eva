using EvaExchange.Dto;
using EvaExchange.Entities;

namespace EvaExchange.Interfaces
{
    public interface ITradeRepository
    {
        public List<Trade> GetAll();
        public Trade GetById(int id);
        public Trade GetByShareIdAndPortfolioId(int shareId, int portfolioId);

        public void Add(Trade trade);
        public void Update(Trade trade);
        public void Delete(int id);
        public void Delete(TradeDto tradeDto);
        public int GetTotalShares(int portfolioId, int shareId);
        
    }
}
