using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrbanTech.Models;

namespace UrbanTech.Controllers
{
    public class HealthController : Controller
    {
        private readonly ILogger<HealthController> _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Health()
        {
           return Ok();
        }

    }
}
