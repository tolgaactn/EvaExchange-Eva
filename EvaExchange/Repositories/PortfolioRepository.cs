using EvaExchange.Data;
using EvaExchange.Entities;
using EvaExchange.Interfaces;

namespace EvaExchange.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly EvaDbContext _context;

        public PortfolioRepository(EvaDbContext context)
        {
            _context = context;
        }

        public void Add(Portfolio portfolio)
        {
            _context.Add(portfolio);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Remove(id);
            _context.SaveChanges();
        }

        public List<Portfolio> GetAll()
        {
            return _context.Portfolios.ToList();
        }

        public Portfolio GetById(int id)
        {
            return _context.Portfolios.Find(id);
        }

        public void Update(Portfolio portfolio)
        {
            _context.Update(portfolio);
            _context.SaveChanges();
        }
    }
}
