using System;
using System.ComponentModel.DataAnnotations;

namespace AdPortal.Infrastructure.Command.UserAd
{
    public class CreateUserAd : AuthenticatedBaseCommand
    {
        
        public Guid CategoryID {get;set;}
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}