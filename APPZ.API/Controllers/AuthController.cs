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
            if(await _authService.Authentificate(authDTO))
            {
                await Authentificate(authDTO.Email);
                return Ok();
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                await _authService.Register(user);
                await Authentificate(user.Email);
            }
            catch(ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }

            return Ok();
        }

        private async Task Authentificate(string email)
        {
            var claims = new List<Claim>()
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, email)
                };
            if(await _authService.IsAdminAsync(email))
            {
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
