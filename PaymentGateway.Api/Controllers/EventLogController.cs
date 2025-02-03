using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Infrastructure.Data;

namespace PaymentGateway.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventLogController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public EventLogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("logs")]
        public async Task<IActionResult> GetEventLogs()
        {
            var logs = await _dbContext.EventLogs.ToListAsync();
            return Ok(logs);
        }
    }
}
