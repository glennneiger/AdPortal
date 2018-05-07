using System.Threading.Tasks;
using AdPortal.Infrastructure.Command;
using AdPortal.Infrastructure.Command.AdminAd;
using AdPortal.Infrastructure.Command.UserAd;
using AdPortal.Infrastructure.Services;

namespace AdPortal.Infrastructure.Handlers.AdminAd
{
    public class ChangeAdStatusHandler : ICommandHandler<ChangeAdStatus>
    {
        private readonly IAdminService _adminService;

        public ChangeAdStatusHandler(IAdminService  adminService)
        {
            _adminService = adminService;
        }
        public async Task HandleAsync(ChangeAdStatus Command)
        {
            await _adminService.ChangeAdStatusAsync(Command.Id,Command.Status);
        }
    }
}