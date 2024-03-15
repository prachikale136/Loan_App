using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LoanApplication.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        public ServicesController()
        {
        }

        // GET api/Services
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            //RecurringJob.AddOrUpdate("Updating Order Screen", () => RefreshOrderAsync("hello from harmony"), Cron.Minutely);
            //RecurringJob.AddOrUpdate<OrderHub>("Boardcast Message", x => x.Send("Hello From Hangfire"), Cron.Minutely);

            return Ok(new { Status = "Ok" });
        }
    }
}
