using EvaExchange.Entities;
using EvaExchange.Interfaces;
using EvaExchange.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EvaExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioShareController : Controller
    {
        private readonly IPortfolioShareRepository _portfolioShareRepository;

        public PortfolioShareController(IPortfolioShareRepository portfolioShareRepository)
        {
            _portfolioShareRepository = portfolioShareRepository;
        }
        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortfolioShare>>> GetAll()
        {
            return _portfolioShareRepository.GetAll();
        }
    }
}
