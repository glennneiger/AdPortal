using System;

namespace AdPortal.Infrastructure.Command
{
    public interface IAuthenticatedCommand : ICommand
    {
        Guid UserId {get;set;}
    }
}