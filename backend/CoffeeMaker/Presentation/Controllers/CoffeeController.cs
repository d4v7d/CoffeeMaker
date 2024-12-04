using CoffeeMaker.Domains.Entities;
using CoffeeMaker.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CoffeeMachineAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoffeeController : ControllerBase
    {
        private readonly CoffeeService coffeeService;

        public CoffeeController(CoffeeService coffeeService)
        {
            this.coffeeService = coffeeService;
        }

        [HttpGet]
        public ActionResult<List<Coffee>> GetAvailableCoffees()
        {
            var coffees = coffeeService.GetAvailableCoffees();
            return Ok(coffees);
        }
    }
}
