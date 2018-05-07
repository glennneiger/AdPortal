using System;

namespace AdPortal.Infrastructure.Command.UserAd
{
    public class RemoveUserAd : AuthenticatedBaseCommand
    {
        public Guid Id {get; set;}
    }
}