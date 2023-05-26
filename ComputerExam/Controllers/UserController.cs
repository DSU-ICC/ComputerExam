using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DomainService.Entity;
using DomainService.DtoModels;
using Microsoft.AspNetCore.Authorization;

namespace ComputerExam.Controllers
{
    [Authorize]
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
        public IActionResult GetEmployees()
        {
            return Ok(_userManager.Users);
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(RegistrationDto model)
        {
            if (ModelState.IsValid)
            {
                Employee user = new() { UserName = model.Login };
                var result = await _userManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
                    return Ok();
            }
            return BadRequest();
        }

        [Route("EditEmployee")]
        [HttpPost]
        public async Task<IActionResult> EditEmployee(EditDto model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = await _userManager.FindByIdAsync(model.Id.ToString());
                if (employee != null)
                {
                    employee.UserName = model.Login;
                    employee.PasswordHash = model.Password;
                }
            }
            return Ok();
        }
    }
}
