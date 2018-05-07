using System;
using AdPortal.Core.Domain;

namespace AdPortal.Infrastructure.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime BirthDate { get; set; }
        public Role Role {get; set;}
    }
}