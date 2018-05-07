using System;

namespace AdPortal.Infrastructure.Command
{
    public class AuthenticatedBaseCommand : IAuthenticatedCommand
    {
        public Guid UserId { get ; set;}
    }
}