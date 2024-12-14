using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ums_api.Constants;
using ums_api.Dtos.Message;
using ums_api.Interfaces;

namespace ums_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // Route -> Create a new message to send to another user
        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<IActionResult> CreateNewMessage([FromBody]CreateMessageDto createMessageDto)
        {
            var result = await _messageService.CreateNewMessageAsync(User, createMessageDto);

            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }

            return StatusCode(result.StatusCode, result.Message);
        }

        // Route -> Get all messages for current user, Either as Sender or as Receiver
        [HttpGet]
        [Route("mine")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GetMessageDto>>> GetMyMessages()
        {
            var messages = await _messageService.GetMyMessagesAsync(User);
            return Ok(messages);
        }

        // Route -> Get all messages with Owner access and Admin access
        [HttpGet]
        [Authorize(Roles = StaticUserRoles.OwnerAdmin)]
        public async Task<ActionResult<IEnumerable<GetMessageDto>>> GetMessages()
        {
            var messages = await _messageService.GetMessagesAsync();
            return Ok(messages);
        }
    }
}
