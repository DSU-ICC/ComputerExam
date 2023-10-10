using DomainService.Entity;

namespace DomainService.DtoModels.Account
{
    public class LoginResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Role? Role { get; set; }
    }
}
