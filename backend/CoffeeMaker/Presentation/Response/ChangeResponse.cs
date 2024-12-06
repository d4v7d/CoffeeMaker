using CoffeeMaker.Domains.Entities;

namespace CoffeeMaker.Presentation.Response
{
    public class ChangeResponse
    {
        public int TotalChange { get; set; }
        public List<Coin> ChangeBreakdown { get; set; }
        public string Message { get; set; }
    }
}
