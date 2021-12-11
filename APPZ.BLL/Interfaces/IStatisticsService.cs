using APPZ.DAL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APPZ.BLL.Interfaces
{
    public interface IStatisticsService
    {
        Task<IEnumerable<StatisticsDTO>> GetAllStatisticsAsync();
        Task<IEnumerable<StatisticsDTO>> GetStatisticsByUserIdAsync(int userId);
    }
}
