using CoffeeMaker.Application.Services;
using CoffeeMaker.Domains.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace CoffeeMakerTests
{
    public class CoffeeServiceTests
    {
        private CoffeeService coffeeService;

        [SetUp]
        public void Setup()
        {
            coffeeService = new CoffeeService();
        }

        [Test]
        public void GetAvailableCoffees_ShouldReturnListOfCoffees()
        {
            var result = coffeeService.GetAvailableCoffees();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Coffee>>(result);
        }

        [Test]
        public void UpdateCoffeeStock_ShouldUpdateStockCorrectly()
        {
            
            var selectedCoffees = new List<Coffee>
            {
                new Coffee { Name = "Americano", Quantity = 2 },
                new Coffee { Name = "Latte", Quantity = 1 }
            };

            var result = coffeeService.UpdateCoffeeStock(selectedCoffees);

            Assert.IsTrue(result);
            var updatedCoffees = coffeeService.GetAvailableCoffees();
            Assert.AreEqual(8, updatedCoffees.Find(c => c.Name == "Americano").Stock); 
            Assert.AreEqual(9, updatedCoffees.Find(c => c.Name == "Latte").Stock);   
        }


        [Test]
        public void CalculateTotalCost_ShouldReturnCorrectTotal()
        {
            var selectedCoffees = new List<Coffee>
            {
                new Coffee { Name = "Espresso", Price = 3, Quantity = 2 },
                new Coffee { Name = "Latte", Price = 4, Quantity = 1 }
            };

            var totalCost = coffeeService.CalculateTotalCost(selectedCoffees);

            Assert.AreEqual(10, totalCost);
        }
    }
}