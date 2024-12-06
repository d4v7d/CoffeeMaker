using CoffeeMaker.Application.Services;
using CoffeeMaker.Domains.Entities;
using CoffeeMaker.Presentation.Request;
using CoffeeMaker.Presentation.Response;
using NUnit.Framework;
using System.Collections.Generic;

namespace CoffeeMakerTests
{
    [TestFixture]
    public class PaymentServiceTests
    {
        private PaymentService paymentService;
        private CoffeeService coffeeService;

        [SetUp]
        public void Setup()
        {
            coffeeService = new CoffeeService();
            paymentService = new PaymentService(coffeeService);
        }

        [Test]
        public void MakePurchase_WithValidData_ShouldReturnSuccess()
        {
            var selectedCoffees = new List<Coffee>
            {
                new Coffee { Name = "Americano", Price = 950, Quantity = 1 },
                new Coffee { Name = "Latte", Price = 1350, Quantity = 1 }
            };

            var paymentInput = new List<Coin>
            {
                new Coin { Value = 1000, Quantity = 3 }
            };

            var request = new PurchaseRequest
            {
                SelectedCoffees = selectedCoffees,
                PaymentInput = paymentInput
            };

            var response = paymentService.MakePurchase(request);

            Assert.IsNotNull(response);
            Assert.AreEqual("Purchase successful", response.Message);
            Assert.AreEqual(700, response.TotalChange);
            Assert.IsNotNull(response.ChangeBreakdown);
            Assert.IsTrue(response.ChangeBreakdown.Count > 0);
        }

        [Test]
        public void MakePurchase_WithInvalidData_ShouldReturnError()
        {
            var request = new PurchaseRequest
            {
                SelectedCoffees = null,
                PaymentInput = null
            };

            var response = paymentService.MakePurchase(request);

            Assert.IsNotNull(response);
            Assert.AreEqual("Invalid data", response.Message);
        }

        [Test]
        public void MakePurchase_WithInsufficientPayment_ShouldReturnError()
        {
            var selectedCoffees = new List<Coffee>
            {
                new Coffee { Name = "Americano", Price = 950, Quantity = 1 }
            };

            var paymentInput = new List<Coin>
            {
                new Coin { Value = 500, Quantity = 1 }
            };

            var request = new PurchaseRequest
            {
                SelectedCoffees = selectedCoffees,
                PaymentInput = paymentInput
            };

            var response = paymentService.MakePurchase(request);

            Assert.IsNotNull(response);
            Assert.AreEqual("Insufficient payment", response.Message);
        }

        [Test]
        public void MakePurchase_WhenUnableToProvideChange_ShouldReturnError()
        {
            var selectedCoffees = new List<Coffee>
            {
                new Coffee { Name = "Americano", Price = 950, Quantity = 1 }
            };

            var paymentInput = new List<Coin>
            {
                new Coin { Value = 1000, Quantity = 1 }
            };

            paymentService = new PaymentService(coffeeService);
            var emptyCoins = new List<Coin>();
            typeof(PaymentService)
                .GetField("coins", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(paymentService, emptyCoins);

            var request = new PurchaseRequest
            {
                SelectedCoffees = selectedCoffees,
                PaymentInput = paymentInput
            };

            var response = paymentService.MakePurchase(request);

            Assert.IsNotNull(response);
            Assert.AreEqual("Unable to provide change", response.Message);
        }
    }
}
