using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DomainService.Entity;
using DomainService.DtoModels.Account;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<Employee> _signInManager;
        private readonly UserManager<Employee> _userManager;

        public AccountController(SignInManager<Employee> signInManager, UserManager<Employee> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
                var employee = _userManager.Users.FirstOrDefault(x => x.UserName == loginDto.Login);

                var result =
                    await _signInManager.PasswordSignInAsync(loginDto.Login, loginDto.Password, false, false);
                if (result.Succeeded)
                    return Ok(employee);
                else
                    return BadRequest("Неправильный логин и (или) пароль");
            }
            return BadRequest();
        }
    }
}
