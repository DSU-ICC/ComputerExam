using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DomainService.Entity;
using DomainService.DtoModels;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<Employee> _signInManager;

        public AccountController(SignInManager<Employee> signInManager)
        {
            _signInManager = signInManager;
        }

        [Route("Logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
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
                    return Ok();
                else
                    return BadRequest("Неправильный логин и (или) пароль");
            }
            return BadRequest();
        }
    }
}
