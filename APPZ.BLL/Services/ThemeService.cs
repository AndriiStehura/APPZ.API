using APPZ.BLL.Interfaces;
using APPZ.DAL;
using APPZ.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APPZ.BLL.Services
{
    public class ThemeService: IThemeService
    {
        readonly IUnitOfWork _unit;

        public ThemeService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<IEnumerable<TaskTheme>> GetAllThemesAsync() =>
            await _unit.ThemesRepository.GetAsync(include: x => x.Include(y => y.Tasks));

        public async Task<TaskTheme> GetThemeByIdAsync(int id) =>
            (await _unit.ThemesRepository.GetAsync(
                x => x.Id == id,
                include: x => x.Include(y => y.Tasks)))
            .FirstOrDefault();

        public async Task AddThemeAsync(TaskTheme theme)
        {
            await _unit.ThemesRepository.InsertAsync(theme);
            await _unit.SaveAsync();
        }

        public async Task UpdateThemeAsync(TaskTheme theme)
        {
            _unit.ThemesRepository.Update(theme);
            await _unit.SaveAsync();
        }

        public async Task DeleteThemeAsync(int themeId)
        {
            await _unit.ThemesRepository.Delete(themeId);
            await _unit.SaveAsync();
        }
    }
}
