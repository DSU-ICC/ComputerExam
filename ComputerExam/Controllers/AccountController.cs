using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DomainService.Entity;
using DomainService.DtoModels;
using Sentry;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;

        public AccountController(UserManager<Employee> userManager, SignInManager<Employee> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationDto registrationDto)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new() { UserName = registrationDto.Login, TeacherId = registrationDto.TeacherId };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(employee, registrationDto.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(employee, false);
                    return Ok();
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return Ok();
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(loginDto.Login, loginDto.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return BadRequest();
        }

        [Route("Logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
