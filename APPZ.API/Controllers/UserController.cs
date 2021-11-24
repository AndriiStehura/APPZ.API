using APPZ.BLL.Interfaces;
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

        [HttpGet("all")]
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

        [HttpGet("new")]
        public async Task<IActionResult> GetUser([FromBody] User user)
        {
            await _userService.AddUserAsync(user);
            return Ok();
        }
    }
}
