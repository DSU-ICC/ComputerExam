using System.ComponentModel;

namespace DomainService.Entity
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        [PasswordPropertyText]
        public string Password { get; set; } = null!;
        public Role? Role { get; set; }
        public Guid? RoleId { get; set; }
    }
}
