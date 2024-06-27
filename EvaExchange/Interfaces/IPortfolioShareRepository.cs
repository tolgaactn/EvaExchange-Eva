using EvaExchange.Entities;

namespace EvaExchange.Interfaces
{
    public interface IPortfolioShareRepository
    {
        public List<PortfolioShare> GetAll();
        public PortfolioShare GetById(int id);
        public PortfolioShare GetByShareIdAndPortfolioId(int shareId,int portfolioId);

        public void Add(PortfolioShare portfolioShare);
        public void Update(PortfolioShare portfolioShare);
        public void Delete(int id);
    }
}
