using DotnetCoreFirst.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IGreeter _greeter;
        public HomeController(IGreeter _greeter)
        {
            this._greeter = _greeter;
        }

        [HttpGet]
        public IActionResult GreetFirstTime()
        {
            return Ok(_greeter.Greet());
        }
    }
}
