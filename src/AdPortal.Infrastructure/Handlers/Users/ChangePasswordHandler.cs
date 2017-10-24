using System;
using System.Threading.Tasks;
using AdPortal.Infrastructure.Command;
using AdPortal.Infrastructure.Command.Users;
using AdPortal.Infrastructure.Services;

namespace AdPortal.Infrastructure.Handlers.Users
{
    public class ChangePasswordHandler : ICommandHandler<ChangePassword>
    {
        private readonly IUserService _userService;
        public ChangePasswordHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task HandleAsync(ChangePassword Command)
        {
            if(Command.NewPassword != Command.NewPasswordConfirmation)
            {
                throw new Exception("New password confirmation must be equal new password.");
            }
            await _userService.ChangePassword(Command.UserId, Command.NewPassword, Command.OldPassword);
        }
    }
}