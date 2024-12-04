using CoffeeMaker.Presentation.Response;
using CoffeeMaker.Presentation.Request;
using CoffeeMaker.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachineAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly CoffeeService coffeeService;
        private readonly PaymentService paymentService;

        public PurchaseController(CoffeeService coffeeService, PaymentService paymentService)
        {
            this.coffeeService = coffeeService;
            this.paymentService = paymentService;
        }

        [HttpPost]
        public ActionResult<ChangeResponse> MakePurchase([FromBody] PurchaseRequest request)
        {
            if (paymentService.IsOutOfService())
            {
                return BadRequest(new ChangeResponse { Message = "Fuera de servicio" });
            }

            if (!paymentService.ValidatePaymentInput(request.PaymentInput))
            {
                return BadRequest(new ChangeResponse { Message = "Denominaciones inválidas" });
            }

            foreach (var item in request.SelectedCoffees)
            {
                var coffee = coffeeService.GetAvailableCoffees().Find(c => c.Name == item.Key);
                if (coffee == null || coffee.Quantity < item.Value)
                {
                    return BadRequest(new ChangeResponse { Message = $"No hay suficiente {item.Key}" });
                }
            }

            int totalCost = coffeeService.CalculateTotalCost(request.SelectedCoffees);
            int totalPayment = paymentService.CalculateTotalPayment(request.PaymentInput);

            if (totalPayment < totalCost)
            {
                return BadRequest(new ChangeResponse { Message = "Fondos insuficientes" });
            }

            int changeAmount = totalPayment - totalCost;
            var changeBreakdown = paymentService.CalculateChange(changeAmount);

            if (changeBreakdown == null)
            {
                return BadRequest(new ChangeResponse { Message = "Fallo al realizar la compra" });
            }

            coffeeService.UpdateCoffeeStock(request.SelectedCoffees);
            paymentService.AddPaymentToCoins(request.PaymentInput);

            return Ok(new ChangeResponse
            {
                TotalChange = changeAmount,
                ChangeBreakdown = changeBreakdown,
                Message = $"Compra realizada con éxito. Su vuelto es de {changeAmount} colones."
            });
        }
    }
}
