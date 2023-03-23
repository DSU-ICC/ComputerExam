using Microsoft.AspNetCore.Identity;

namespace DomainService.Entity
{
    public class Employee : IdentityUser
    {
        public int TeacherId { get; set; }
    }
}
