using APPZ.BLL.Interfaces;
using APPZ.DAL;
using APPZ.DAL.DTO;
using APPZ.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APPZ.BLL.Services
{
    public class StatisticsService: IStatisticsService
    {
        readonly IUnitOfWork _unit;
        
        public StatisticsService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<IEnumerable<StatisticsDTO>> GetAllStatisticsAsync() =>
            (await _unit.StatisticsRepository.GetAsync(
                include: x => x.Include(y => y.Task)
                    .ThenInclude(y => y.Theme)
                    .Include(y => y.User)))
            .Select(x => new StatisticsDTO
            {
                Id = x.Id,
                Date = x.Date.ToString("o"),
                Grade = x.Grade,
                Task = x.Task,
                TaskId = x.TaskId,
                User = x.User,
                UserId = x.UserId
            }).ToList();

        public async Task<IEnumerable<StatisticsDTO>> GetStatisticsByUserIdAsync(int userId) =>
            (await _unit.StatisticsRepository.GetAsync(
                x => x.UserId == userId,
                include: x => x.Include(y => y.Task)
                    .ThenInclude(y => y.Theme)
                    .Include(y => y.User)))
            .Select(x => new StatisticsDTO
            {
                Id = x.Id,
                Date = x.Date.ToString("o"),
                Grade = x.Grade,
                Task = x.Task,
                TaskId = x.TaskId,
                User = x.User,
                UserId = x.UserId
            }).ToList();
    }
}
