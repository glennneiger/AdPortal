using System;

namespace AdPortal.Infrastructure.Command.UserAd
{
    public class EditUserAd : AuthenticatedBaseCommand
    {
        public Guid Id {get;set;}
        public Guid CategoryId {get;set;}
        public string Name {get; set;}
        public string Content {get; set;}

    }
}