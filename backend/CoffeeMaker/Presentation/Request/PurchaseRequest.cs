namespace CoffeeMaker.Presentation.Request
{
    public class PurchaseRequest
    {
        public Dictionary<string, int> SelectedCoffees { get; set; }
        public Dictionary<int, int> PaymentInput { get; set; } // Valor y cantidad
    }
}
