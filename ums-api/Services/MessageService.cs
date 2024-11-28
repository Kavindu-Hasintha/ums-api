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

        public Task<GeneralServiceResponseDto> CreateNewMessageAsync(ClaimsPrincipal User, CreateMessageDto createMessageDto)
        {
            throw new NotImplementedException();
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
