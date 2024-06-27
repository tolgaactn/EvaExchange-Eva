using EvaExchange.Data;
using EvaExchange.Entities;
using EvaExchange.Interfaces;

namespace EvaExchange.Repositories
{
    public class PortfolioShareRepository : IPortfolioShareRepository
    {
        private readonly EvaDbContext _context;

        public PortfolioShareRepository(EvaDbContext context)
        {
            _context = context;
        }

        public void Add(PortfolioShare portfolioShare)
        {
            _context.Add(portfolioShare);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Remove(id);
        }

        public List<PortfolioShare> GetAll()
        {
            var portfolioShares = _context.PortfolioShares.ToList();
            return portfolioShares;
        }

        public PortfolioShare GetById(int id)
        {
            
            return _context.PortfolioShares.Find(id);

        }

        public PortfolioShare GetByShareIdAndPortfolioId(int shareId, int portfolioId)
        {
            var portfolioShare = _context.PortfolioShares
            .Where(t => t.ShareId == shareId && t.PortfolioId == portfolioId)
            .FirstOrDefault();
            return portfolioShare;
        }

        public void Update(PortfolioShare portfolioShare)
        {
            _context.Update(portfolioShare);
            _context.SaveChanges();
        }
    }
}
