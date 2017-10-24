using System.Threading.Tasks;
using AdPortal.Infrastructure.Command;
using AdPortal.Infrastructure.Command.Users;
using AdPortal.Infrastructure.Services;

namespace AdPortal.Infrastructure.Handlers.Users
{
    public class LoginHandler : ICommandHandler<Login>
    {
        private readonly IUserService _userService;
        public LoginHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task HandleAsync(Login Command)
        {
            await _userService.LoginAsync(Command.Email,Command.Password);
        }
    }
}