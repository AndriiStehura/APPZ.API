using APPZ.BLL.Interfaces;
using APPZ.DAL.DTO;
using APPZ.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APPZ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> NewUser([FromBody] User user)
        {
            await _userService.AddUserAsync(user);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO userDto)
        {
            await _userService.UpdateUserAsync(userDto);
            return Ok();
        }

        [HttpPut("password")]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordDTO passwordDTO)
        {
            await _userService.UpdatePasswordAsync(passwordDTO);
            return Ok();
        }
    }
}
