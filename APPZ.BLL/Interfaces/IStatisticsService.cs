using APPZ.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APPZ.BLL.Interfaces
{
    public interface IStatisticsService
    {
        Task<IEnumerable<TaskStatistics>> GetAllStatisticsAsync();
        Task<IEnumerable<TaskStatistics>> GetStatisticsByUserIdAsync(int userId);
    }
}
