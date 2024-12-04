using CoffeeMaker.Domains.Entities;

namespace CoffeeMaker.Application.Services
{
    public class PaymentService
    {
        private List<Coin> coins;
        private readonly int[] Values = { 1000, 500, 100, 50, 25 };

        public PaymentService()
        {
            coins = new List<Coin>
            {
                new Coin { Value = 500, Quantity = 20 },
                new Coin { Value = 100, Quantity = 30 },
                new Coin { Value = 50, Quantity = 50 },
                new Coin { Value = 25, Quantity = 25 }
            };
        }

        public bool IsOutOfService()
        {
            return coins.All(c => c.Quantity == 0);
        }

        public bool ValidatePaymentInput(Dictionary<int, int> paymentInput)
        {
            return paymentInput.Keys.All(k => Values.Contains(k));
        }

        public int CalculateTotalPayment(Dictionary<int, int> paymentInput)
        {
            int totalPayment = 0;
            foreach (var item in paymentInput)
            {
                totalPayment += item.Key * item.Value;
            }
            return totalPayment;
        }

        public Dictionary<int, int> CalculateChange(int changeAmount)
        {
            var changeBreakdown = new Dictionary<int, int>();
            var sortedCoins = coins.OrderByDescending(c => c.Value).ToList();

            foreach (var coin in sortedCoins)
            {
                int numberOfCoins = changeAmount / coin.Value;
                if (numberOfCoins > 0)
                {
                    int coinsToGive = numberOfCoins <= coin.Quantity ? numberOfCoins : coin.Quantity;
                    if (coinsToGive > 0)
                    {
                        changeBreakdown[coin.Value] = coinsToGive;
                        changeAmount -= coinsToGive * coin.Value;
                        coin.Quantity -= coinsToGive;
                    }
                }
            }

            if (changeAmount > 0)
            {
                return null;
            }

            return changeBreakdown;
        }

        public void AddPaymentToCoins(Dictionary<int, int> paymentInput)
        {
            foreach (var item in paymentInput)
            {
                var coin = coins.FirstOrDefault(c => c.Value == item.Key);
                if (coin != null)
                {
                    coin.Quantity += item.Value;
                }
                else
                {
                    coins.Add(new Coin { Value = item.Key, Quantity = item.Value });
                }
            }
        }

        public List<Coin> GetAvailableCoins()
        {
            return coins;
        }
    }
}
