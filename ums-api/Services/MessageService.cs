using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ums_api.DbContext;
using ums_api.Dtos.General;
using ums_api.Dtos.Message;
using ums_api.Entities;
using ums_api.Interfaces;

namespace ums_api.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogService _logService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageService(ApplicationDbContext context, ILogService logService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logService = logService;
            _userManager = userManager;
        }

        public async Task<GeneralServiceResponseDto> CreateNewMessageAsync(ClaimsPrincipal User, CreateMessageDto createMessageDto)
        {
            if (User.Identity.Name == createMessageDto.ReceiverUsername)
            {
                return new GeneralServiceResponseDto()
                {
                    IsSucceed = false,
                    StatusCode = 400,
                    Message = "Sender and Receiver can not be same",
                };
            }

            var isReceiverUsernameValid = _userManager.Users.Any(q => q.UserName == createMessageDto.ReceiverUsername);
            if (!isReceiverUsernameValid)
            {
                return new GeneralServiceResponseDto()
                {
                    IsSucceed = false,
                    StatusCode = 400,
                    Message = "Receiver Username is not valid",
                };
            }

            Message newMessage = new Message()
            {
                SenderUserName = User.Identity.Name,
                ReceiverUserName = createMessageDto.ReceiverUsername,
                Text = createMessageDto.Text,
            };
            await _context.Messages.AddAsync(newMessage);
            await _context.SaveChangesAsync();
            await _logService.SaveNewLog(User.Identity.Name, "Send Message");

            return new GeneralServiceResponseDto()
            {
                IsSucceed = true,
                StatusCode = 201,
                Message = "Message saved successfully",
            };
        }

        public Task<IEnumerable<GetMessageDto>> GetMessagesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetMessageDto>> GetMyMessagesAsync(ClaimsPrincipal User)
        {
            throw new NotImplementedException();
        }
    }
}
