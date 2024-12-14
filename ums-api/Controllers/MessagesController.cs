using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


    }
}
