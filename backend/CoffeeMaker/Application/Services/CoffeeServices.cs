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
                    new Coffee { Name = "Americano", Price = 950, Stock = 10, Quantity = 0 },
                    new Coffee { Name = "Capuchino", Price = 1200, Stock = 8, Quantity = 0 },
                    new Coffee { Name = "Latte", Price = 1350, Stock = 10 , Quantity = 0},
                    new Coffee { Name = "Mocachino", Price = 1500, Stock = 15, Quantity = 0 }
                };
        }

        public List<Coffee> GetAvailableCoffees()
        {
            return coffees;
        }

        public bool UpdateCoffeeStock(List<Coffee> selectedCoffees)
        {
            foreach (var item in selectedCoffees)
            {
                var coffee = coffees.FirstOrDefault(c => c.Name == item.Name);
                if (coffee != null)
                {
                    coffee.Stock -= item.Quantity;
                }
            }
            return true;
        }

        public int CalculateTotalCost(List<Coffee> selectedCoffees)
        {
            int totalCost = 0;
            foreach (var item in selectedCoffees)
            {
                totalCost += item.Price * item.Quantity;
            }
            return totalCost;
        }
    }
}
