using System.Security.Claims;
using ums_api.DbContext;
using ums_api.Dtos.Log;
using ums_api.Entities.Log;
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
        public async Task SaveNewLog(string Username, string Description)
        {
            var newLog = new Log()
            {
                Username = Username,
                Description = Description
            };
            await _context.Logs.AddAsync(newLog);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetLogDto>> GetLogsAsync()
        {
            var logs = await _context.Logs
                .Select(q => new GetLogDto
                {
                    CreatedAt = q.CreatedAt,
                    Description = q.Description,
                    UserName = q.UserName,
                })
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
            return logs;
        }

        public Task<IEnumerable<GetLogDto>> GetMyLogsAsync(ClaimsPrincipal User)
        {
            throw new NotImplementedException();
        }
    }
}
