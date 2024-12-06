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
        private readonly PaymentService paymentService;

        public PurchaseController(PaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        [HttpPost]
        public ActionResult<ChangeResponse> MakePurchase(PurchaseRequest request)
        {
            var response = paymentService.MakePurchase(request);
            return Ok(response);
        }
    }
}
