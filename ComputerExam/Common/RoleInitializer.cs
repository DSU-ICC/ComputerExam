using DomainService.Entity;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace DomainService.DBService
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(string adminLogin, string password, EmployeeRepository employeeManager, RoleRepository roleManager)
        {
            if (roleManager.Get().FirstOrDefault(x => x.Name == "admin") == null)
            {
                await roleManager.Create(new Role("admin"));
            }
            if (employeeManager.Get().FirstOrDefault(x => x.Name == adminLogin) == null)
            {
                Employee admin = new() { Name = adminLogin, Password = password, RoleId = roleManager.Get().FirstOrDefault(x=>x.Name == "admin")?.Id };
            }
        }
    }
}
