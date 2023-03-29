using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DomainService.Entity;
using Sentry;
using Microsoft.EntityFrameworkCore;
using DomainService.DtoModels;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<Employee> _userManager;

        public UserController(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }


        [Route("GetEmployees")]
        [HttpGet]
        public ActionResult<Employee> GetEmployees()
        {
            return Ok(_userManager.Users);
        }

        [Route("EditEmployee")]
        [HttpPut]
        public async Task<ActionResult<Employee>> EditEmployee(EditDto _editDto)
        {
            if (ModelState.IsValid)
            {
                Employee employee = await _userManager.FindByIdAsync(_editDto.Id.ToString());
                if (employee != null)
                {
                    employee.UserName = _editDto.Login;
                    employee.PasswordHash = _editDto.Password;
                }
            }
            return Ok();
        }
    }
}
