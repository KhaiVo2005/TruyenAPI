using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TruyenAPI.Models.DTOs;

namespace TruyenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<IdentityUser> user;

        public RegisterController(UserManager<IdentityUser> user)
        {
            this.user = user;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO userDTO)
        {
            if (userDTO == null) return BadRequest("Chua nhap");
            IdentityUser identityUser = new IdentityUser
            {
                Email = userDTO.EmailAddress,
                UserName = userDTO.UserName
            };
            var result = await user.CreateAsync(identityUser, userDTO.Password);
            if (result.Succeeded)
                if (userDTO.Role != null && userDTO.Role.Any())
                {
                    result = await user.AddToRolesAsync(identityUser, userDTO.Role);
                    if (result.Succeeded) return Ok("Register success");
                }
            return BadRequest("Something are wrong data type");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
        {
            var userLog = await user.FindByEmailAsync(userDTO.EmailAddress);
            if (userLog != null)
            {
                var checkPass = await user.CheckPasswordAsync(userLog, userDTO.Password);
                if (checkPass)
                {
                    var role = await user.GetRolesAsync(userLog);
                    return Ok($"Welcome {role[0]}");
                }
            }
            return BadRequest("Wrong user or password");
        }
    }
}
