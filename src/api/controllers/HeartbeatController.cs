using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace amazon_customer_reviews_api.controllers
{
    [Route("heartbeat")]
    [ApiController]
    public class HeartbeatController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public HeartbeatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok();
        }
    }
}
