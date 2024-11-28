using System.Security.Claims;
using ums_api.Dtos.Log;

namespace ums_api.Interfaces
{
    public interface ILogService
    {
        Task SaveNewLog(string Username, string Description);
        Task<IEnumerable<GetLogDto>> GetLogsAsync();
        Task<IEnumerable<GetLogDto>> GetMyLogsAsync(ClaimsPrincipal User);
    }
}
