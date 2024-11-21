using System.Security.Claims;
using ums_api.Dtos.General;
using ums_api.Dtos.Message;

namespace ums_api.Interfaces
{
    public interface IMessageService
    {
        Task<GeneralServiceResponseDto> CreateNewMessageAsync(ClaimsPrincipal User, CreateMessageDto createMessageDto);
        Task<IEnumerable<GetMessageDto>> GetMessagesAsync();
        Task<IEnumerable<GetMessageDto>> GetMyMessagesAsync(ClaimsPrincipal User);
    }
}
