using APPZ.BLL.Interfaces;
using APPZ.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APPZ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemesController : ControllerBase
    {
        readonly IThemeService _themeService;
        public ThemesController(IThemeService themeService)
        {
            _themeService = themeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllThemes() =>
            Ok(await _themeService.GetAllThemesAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTheme([FromRoute] int id) =>
            Ok(await _themeService.GetThemeByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> AddTheme([FromBody] TaskTheme theme)
        {
            await _themeService.AddThemeAsync(theme);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTheme([FromBody] TaskTheme theme)
        {
            await _themeService.UpdateThemeAsync(theme);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTheme([FromRoute] int id)
        {
            await _themeService.DeleteThemeAsync(id);
            return Ok();
        }
    }
}
