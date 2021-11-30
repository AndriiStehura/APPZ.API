using APPZ.BLL.Interfaces;
using APPZ.DAL.DTO;
using APPZ.DAL.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace APPZ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(AuthDTO authDTO)
        {
            var user = await _authService.Authentificate(authDTO);
            if (user != null)
            {
                await Authentificate(user);
                return Ok(user);
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            UserDTO dto = null;
            try
            {
                dto = await _authService.Register(user);
                await Authentificate(dto);
            }
            catch(ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }

            return Ok(dto);
        }

        private async Task Authentificate(UserDTO user)
        {
            var claims = new List<Claim>()
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
                };
            if(await _authService.IsAdminAsync(user.Email))
            {
                user.IsAdmin = true;
                claims.Add(new Claim("role", "admin"));
            }
            var identity = new ClaimsIdentity(
                claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
            );

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity)
            );
        }

        [HttpPost("signout/{email}")]
        public async Task<IActionResult> SignOut(string email)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
