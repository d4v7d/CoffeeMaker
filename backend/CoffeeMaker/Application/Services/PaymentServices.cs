using CoffeeMaker.Domains.Entities;
using CoffeeMaker.Presentation.Response;
using CoffeeMaker.Presentation.Request;

namespace CoffeeMaker.Application.Services
{
    public class PaymentService
    {
        private List<Coin> coins;
        private readonly int[] Values = { 1000, 500, 100, 50, 25 };
        private readonly CoffeeService coffeeService;

        public PaymentService(CoffeeService coffeeService)
        {
            this.coffeeService = coffeeService;
            coins = new List<Coin>
                {
                    new Coin { Value = 500, Quantity = 20 },
                    new Coin { Value = 100, Quantity = 30 },
                    new Coin { Value = 50, Quantity = 50 },
                    new Coin { Value = 25, Quantity = 25 }
                };
        }

        public ChangeResponse MakePurchase(PurchaseRequest request)
        {
            if (!ValidateData(request))
                return new ChangeResponse { Message = "Invalid data" };

            int totalPayment = CalculateTotalPayment(request.PaymentInput);
            int totalCost = CalculateTotalCost(request.SelectedCoffees);

            if (totalPayment < totalCost)
                return new ChangeResponse { Message = "Insufficient payment" };

            UpdateCoins(request.PaymentInput, true);
            int changeAmount = totalPayment - totalCost;
            List<Coin> change = CalculateChange(changeAmount);

            if (change == null)
                return new ChangeResponse { Message = "Unable to provide change" };

            UpdateCoins(change, false);
            UpdateCoffeeStock(request.SelectedCoffees);

            return new ChangeResponse
            {
                TotalChange = changeAmount,
                ChangeBreakdown = change,
                Message = "Purchase successful"
            };
        }

        private bool ValidateData(PurchaseRequest request)
        {
            // Validate request data
            return request.SelectedCoffees != null && request.PaymentInput != null;
        }

        private int CalculateTotalPayment(List<Coin> paymentInput)
        {
            return paymentInput.Sum(coin => coin.Value * coin.Quantity);
        }

        private int CalculateTotalCost(List<Coffee> selectedCoffees)
        {
            return coffeeService.CalculateTotalCost(selectedCoffees);
        }

        private void UpdateCoins(List<Coin> paymentInput, bool isAdding)
        {
            // Update coins
            foreach (var paymentCoin in paymentInput)
            {
                var coin = coins.FirstOrDefault(c => c.Value == paymentCoin.Value);
                if (coin != null)
                {
                    coin.Quantity += isAdding ? paymentCoin.Quantity : -paymentCoin.Quantity;
                }
            }
        }

        private List<Coin> CalculateChange(int changeAmount)
        {
            // Calculate change
            List<Coin> change = new List<Coin>();
            foreach (var value in Values)
            {
                if (changeAmount == 0) break;
                var coin = coins.FirstOrDefault(c => c.Value == value && c.Quantity > 0);
                if (coin != null)
                {
                    int numCoins = Math.Min(changeAmount / value, coin.Quantity);
                    if (numCoins > 0)
                    {
                        change.Add(new Coin { Value = value, Quantity = numCoins });
                        changeAmount -= numCoins * value;
                    }
                }
            }
            return changeAmount == 0 ? change : null;
        }

        private void UpdateCoffeeStock(List<Coffee> selectedCoffees)
        {
            coffeeService.UpdateCoffeeStock(selectedCoffees);
        }
    }
}
