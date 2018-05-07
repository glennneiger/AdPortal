using System.Threading.Tasks;
using AdPortal.Infrastructure.Command;
using AdPortal.Infrastructure.Command.UserAd;
using AdPortal.Infrastructure.Services;

namespace AdPortal.Infrastructure.Handlers.UserAd
{
    public class EditUserAdHandler : ICommandHandler<EditUserAd>
    {
        private readonly IUserAdService _userAdService;
        public EditUserAdHandler(IUserAdService userAdService)
        {
            _userAdService = userAdService;
        }
        public async Task HandleAsync(EditUserAd Command)
        {
            await _userAdService.EditAdAsync(Command.UserId, Command.Id, Command.CategoryId, Command.Name, Command.Content);
        }
    }
}