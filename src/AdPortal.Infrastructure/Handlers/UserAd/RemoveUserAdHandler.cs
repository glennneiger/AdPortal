using System.Threading.Tasks;
using AdPortal.Infrastructure.Command;
using AdPortal.Infrastructure.Command.UserAd;
using AdPortal.Infrastructure.Services;

namespace AdPortal.Infrastructure.Handlers.UserAd
{
    public class RemoveUserAdHandler : ICommandHandler<RemoveUserAd>
    {
        private readonly IUserAdService _userAdService;
        public RemoveUserAdHandler(IUserAdService userAdService)
        {
            _userAdService = userAdService;
        }
        public async Task HandleAsync(RemoveUserAd Command)
        {
            await _userAdService.RemoveAsync(Command.UserId, Command.Id);
        }
    }
}