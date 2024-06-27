namespace EvaExchange.Entities
{
    public class Portfolio
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public int UserId { get; set; }
        
        

        

        //Relations
        public User User { get; set; }
        public List<Trade> Trades { get; set; }
        public List<PortfolioShare > PortfolioShares { get; set; }
    }
}
