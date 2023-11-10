using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Operation.Hubs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Api.Controllers
{
    [ApiController]
    [Route("oa/api/v1/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IHubContext<MessageHub> _messageHubContext;
        private readonly IMediator mediator;

        public MessageController(IHubContext<MessageHub> messageHubContext, IMediator mediator)
        {
            _messageHubContext = messageHubContext;
            this.mediator = mediator;
        }

        [HttpPost("send")]
        [Authorize(Roles = "admin, dealer")]
        public IActionResult SendMessage(string receiverId, string content)
        {
            _messageHubContext.Clients.User(receiverId)?.SendAsync("ReceiveMessage", "admin", content);
            return Ok();
        }

        [HttpGet("ByDealerId")]
        [Authorize(Roles = "dealer, admin")]
        public async Task<ApiResponse<List<MessageResponse>>> GetByDealerId(int dealerid)
        {
            var operation = new GetMessagesByDealerIdQuery(dealerid);

            var result = await mediator.Send(operation);

            return result;
        }
    }
}
