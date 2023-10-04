using System.ComponentModel;

namespace DomainService.Entity
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [PasswordPropertyText]
        public string Password { get; set; } = null!;
        public Role? Role { get; set; }
        public int? RoleId { get; set; }
    }
}
