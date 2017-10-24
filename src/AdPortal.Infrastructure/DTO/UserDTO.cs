using System;

namespace AdPortal.Infrastructure.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}