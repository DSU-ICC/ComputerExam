using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DomainService.Entity;
using Sentry;
using Microsoft.AspNetCore.Authorization;

namespace ComputerExam.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Employee> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<Employee> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Route("GetRoles")]
        [HttpGet]
        public IActionResult GetRoles()
        {
            return Ok(_roleManager.Roles.ToList());
        }

        [Route("CreateRole")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                await _roleManager.CreateAsync(new IdentityRole(name));
            }
            return Ok();
        }

        [Route("EditRole")]
        [HttpPost]
        public async Task<IActionResult> EditRole(string employeeId, List<string> roles)
        {
            Employee employee = await _userManager.FindByIdAsync(employeeId);
            if (employee != null)
            {
                var userRoles = await _userManager.GetRolesAsync(employee);
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(employee, addedRoles);

                await _userManager.RemoveFromRolesAsync(employee, removedRoles);

                return Ok();
            }
            return Ok();
        }

        [Route("DeleteRole")]
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }
            return Ok();
        }
    }
}
