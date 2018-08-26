using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ConsulDemo2.Controllers
{
    [Produces("application/json")]
    [Route("api/Health")]
    public class HealthController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            Console.WriteLine("健康检查" + DateTime.Now);
            return Content("ok demo2");
        }
    }
}
