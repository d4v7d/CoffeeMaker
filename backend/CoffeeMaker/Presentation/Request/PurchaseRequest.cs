using CoffeeMaker.Domains.Entities;

namespace CoffeeMaker.Presentation.Request
{
    public class PurchaseRequest
    {
        public List<Coffee> SelectedCoffees { get; set; }
        public List<Coin> PaymentInput { get; set; } 
    }
}
