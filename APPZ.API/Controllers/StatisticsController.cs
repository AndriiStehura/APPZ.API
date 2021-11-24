using APPZ.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APPZ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        readonly IStatisticsService _statisticsService;
        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStatisticsAsync() =>
            Ok(await _statisticsService.GetAllStatisticsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatisticsByUserId([FromRoute] int id) =>
            Ok(await _statisticsService.GetStatisticsByUserIdAsync(id));
    }
}
