namespace DomainService.Entity
{
    public class Role
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Role(string name)
        {
            Name = name;
        }
    }
}
