using System.Threading.Tasks;
using AdPortal.Infrastructure.Command;
using AdPortal.Infrastructure.Command.UserAd;
using AdPortal.Infrastructure.Services;

namespace AdPortal.Infrastructure.Handlers.UserAd
{
  
    public class CreateUserAdHandler : ICommandHandler<CreateUserAd>
    {
        private readonly IUserAdService _useradService;
        public CreateUserAdHandler(IUserAdService useradService)
        {
            _useradService = useradService;
        }
        public async Task HandleAsync(CreateUserAd command)
        {
            await _useradService.AddAsync(command.UserId,command.CategoryID,command.Name,
            command.Content,command.ExpiryDate);
        }
    }
}