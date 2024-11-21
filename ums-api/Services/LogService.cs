using System.Security.Claims;
using ums_api.DbContext;
using ums_api.Dtos.Log;
using ums_api.Interfaces;

namespace ums_api.Services
{
    public class LogService : ILogService
    {
        private readonly ApplicationDbContext _context;

        public LogService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task SaveNewLog(string Username, string Description)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetLogDto>> GetLogsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetLogDto>> GetMyLogsAsync(ClaimsPrincipal User)
        {
            throw new NotImplementedException();
        }
    }
}
