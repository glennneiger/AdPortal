using System;
using AdPortal.Core.Domain;

namespace AdPortal.Infrastructure.Command.AdminAd
{
    public class ChangeAdStatus : AuthenticatedBaseCommand
    {
        public Guid Id {get ; set;} // Ad id
        public Status Status {get ; set;}
    }
}