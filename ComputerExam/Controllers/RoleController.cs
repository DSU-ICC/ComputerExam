using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DomainService.Entity;
using Sentry;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<Employee> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<Employee> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Route("GetRoles")]
        [HttpGet]
        public ActionResult<IdentityRole> GetRoles()
        {
            return Ok(_roleManager.Roles.ToList());
        }

        [Route("CreateRole")]
        [HttpPost]
        public async Task<ActionResult<IdentityRole>> CreateRole(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                await _roleManager.CreateAsync(new IdentityRole(name));
            }
            return Ok();
        }

        [Route("DeleteRole")]
        [HttpDelete]
        public async Task<ActionResult<IdentityRole>> DeleteRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }
            return Ok();
        }

        [Route("EditRole")]
        [HttpPut]
        public async Task<ActionResult<IdentityRole>> EditRole(string employeeId, List<string> roles)
        {
            Employee employee = await _userManager.FindByIdAsync(employeeId);
            if (employee != null)
            {
                var userRoles = await _userManager.GetRolesAsync(employee);
                var allRoles = _roleManager.Roles.ToList();
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(employee, addedRoles);

                await _userManager.RemoveFromRolesAsync(employee, removedRoles);

                return Ok();
            }
            return Ok();
        }
    }
}
