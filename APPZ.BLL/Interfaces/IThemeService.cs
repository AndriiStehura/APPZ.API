using APPZ.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APPZ.BLL.Interfaces
{
    public interface IThemeService
    {
        Task<IEnumerable<TaskTheme>> GetAllThemesAsync();
        Task<TaskTheme> GetThemeByIdAsync(int id);
        Task AddThemeAsync(TaskTheme theme);
        Task UpdateThemeAsync(TaskTheme theme);
        Task DeleteThemeAsync(int themeId);
    }
}
