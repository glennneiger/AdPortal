using System;
using System.Threading.Tasks;
using AdPortal.Infrastructure.Command;
using AdPortal.Infrastructure.Command.Users;
using AdPortal.Infrastructure.Services;

namespace AdPortal.Infrastructure.Handlers.Users
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IUserService _userService;
        public CreateUserHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task HandleAsync(CreateUser command)
        {
            await _userService.RegisterAsync(command.UserName,command.Email,command.Password,command.Role);
        }
    }
}