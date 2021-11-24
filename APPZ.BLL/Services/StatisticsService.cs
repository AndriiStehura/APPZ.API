using APPZ.BLL.Interfaces;
using APPZ.DAL;
using APPZ.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<IEnumerable<TaskStatistics>> GetAllStatisticsAsync() =>
            await _unit.StatisticsRepository.GetAsync(
                include: x => x.Include(y => y.Task).Include(y => y.User));

        public async Task<IEnumerable<TaskStatistics>> GetStatisticsByUserIdAsync(int userId) =>
            await _unit.StatisticsRepository.GetAsync(
                x => x.UserId == userId,
                include: x => x.Include(y => y.Task).Include(y => y.User));
    }
}
