namespace EvaExchange.Entities
{
    public class PortfolioShare
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public int ShareId {  get; set; }
        public int ShareQuantity { get; set; }
        

        //Relations
        public Portfolio Portfolio { get; set; }
        public List<Share> Shares { get; set;}
    }
}
