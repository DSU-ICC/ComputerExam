using Microsoft.AspNetCore.Mvc;
using DomainService.Entity;
using Microsoft.AspNetCore.Authorization;
using DomainService.DtoModels.Account;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComputerExam.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [Route("GetEmployees")]
        [HttpGet]
        public IActionResult GetEmployees()
        {
            return Ok(_employeeRepository.Get());
        }

        [Route("GetEmployees")]
        [HttpGet]
        public IActionResult GetAuditories()
        {
            return Ok(_employeeRepository.Get().Include(x => x.Role).Where(x => x.Role.Name == "auditorium"));
        }

        [Authorize("admin")]
        [Route("CreateUser")]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(RegistrationDto model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new()
                {
                    Name = model.Login,
                    Password = model.Password,
                    Role = model.Role,
                    RoleId = model.RoleId
                };

                await _employeeRepository.Create(employee);                
                return Ok();
            }
            return BadRequest();
        }

        [Authorize("admin")]
        [Route("EditUser")]
        [HttpPost]
        public async Task<IActionResult> EditEmployee(EditDto model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.Get().FirstOrDefault(x=>x.Id == model.Id);
                if (employee != null)
                {
                    employee.Name = model.Login;
                    employee.Password = model.Password;
                    employee.Role = model.Role;
                    employee.RoleId = model.RoleId;

                    await _employeeRepository.Update(employee);
                }
            }
            return BadRequest();
        }

        [Authorize("admin")]
        [Route("DeleteUser")]
        [HttpPost]
        public async Task<ActionResult> DeleteEmployee(string id)
        {
            Employee employee = _employeeRepository.Get().FirstOrDefault(c => c.Id.ToString() == id);
            if (employee == null)
                return BadRequest("Пользователь не найден");

            await _employeeRepository.Remove(employee);
            return Ok();
        }
    }
}
