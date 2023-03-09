using ComputerExam.Models;
using Microsoft.AspNetCore.Identity;

namespace ComputerExam.DBService
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(string adminLogin, string password, UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await userManager.FindByNameAsync(adminLogin) == null)
            {
                Employee admin = new() { UserName = adminLogin };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "admin");
            }
        }
    }
}
