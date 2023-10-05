using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DomainService.Entity;
using Sentry;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Repositories.Interfaces;

namespace ComputerExam.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("[controller]")]
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public RoleController(IRoleRepository roleRepository, IEmployeeRepository employeeRepository)
        {
            _roleRepository = roleRepository;
            _employeeRepository = employeeRepository;
        }

        [Route("GetRoles")]
        [HttpGet]
        public IActionResult GetRoles()
        {
            return Ok(_roleRepository.Get().ToList());
        }

        [Route("CreateRole")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (!string.IsNullOrEmpty(name))
                await _roleRepository.Create(new Role(name));
            return Ok();
        }

        [Route("EditRole")]
        [HttpPost]
        public async Task<IActionResult> EditRole(int employeeId, int roleId)
        {
            Employee? employee = _employeeRepository.FindById(employeeId);
            if (employee != null)
            {
                employee.RoleId = roleId;
                await _employeeRepository.Update(employee);

                return Ok();
            }
            return BadRequest("Такой пользователь не найден");
        }

        [Route("DeleteRole")]
        [HttpPost]
        public async Task<IActionResult> DeleteRole(int id)
        {
            Role? role = _roleRepository.FindById(id);
            if (role != null)
                await _roleRepository.Remove(role);
            return Ok();
        }
    }
}
