using CoffeeMaker.Domains.Entities;

namespace CoffeeMaker.Application.Services
{
    public class CoffeeService
    {
        private List<Coffee> coffees;

        public CoffeeService()
        {
            coffees = new List<Coffee>
            {
                new Coffee { Name = "Americano", Price = 950, Quantity = 10 },
                new Coffee { Name = "Capuchino", Price = 1200, Quantity = 8 },
                new Coffee { Name = "Latte", Price = 1350, Quantity = 10 },
                new Coffee { Name = "Mocachino", Price = 1500, Quantity = 15 }
            };
        }

        public List<Coffee> GetAvailableCoffees()
        {
            return coffees;
        }

        public bool UpdateCoffeeStock(Dictionary<string, int> selectedCoffees)
        {
            foreach (var item in selectedCoffees)
            {
                var coffee = coffees.FirstOrDefault(c => c.Name == item.Key);
                if (coffee == null || coffee.Quantity < item.Value)
                {
                    return false;
                }
            }

            foreach (var item in selectedCoffees)
            {
                var coffee = coffees.First(c => c.Name == item.Key);
                coffee.Quantity -= item.Value;
            }

            return true;
        }

        public int CalculateTotalCost(Dictionary<string, int> selectedCoffees)
        {
            int totalCost = 0;
            foreach (var item in selectedCoffees)
            {
                var coffee = coffees.FirstOrDefault(c => c.Name == item.Key);
                if (coffee != null)
                {
                    totalCost += coffee.Price * item.Value;
                }
            }
            return totalCost;
        }
    }
}
