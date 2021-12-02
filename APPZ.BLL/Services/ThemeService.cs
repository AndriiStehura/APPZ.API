using APPZ.BLL.Interfaces;
using APPZ.DAL;
using APPZ.DAL.DTO;
using APPZ.DAL.Entities;
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

        public async Task<IEnumerable<ThemeDTO>> GetAllThemesAsync() =>
            (await _unit.ThemesRepository.GetAsync())
            .Select(x => new ThemeDTO 
            {
                Id = x.Id,
                Name = x.Name,
                Tasks = _unit.TasksRepository.GetAsync(y => y.ThemeId == x.Id).Result
            })
            .ToList();

        public async Task<ThemeDTO> GetThemeByIdAsync(int id)
        {
            ThemeDTO dto = null;
            var theme = (await _unit.ThemesRepository.GetAsync()).FirstOrDefault();
            if(theme != null)
            {
                var tasks = await _unit.TasksRepository.GetAsync(y => y.ThemeId == theme.Id);
                dto = new ThemeDTO
                {
                    Id = theme.Id,
                    Name = theme.Name,
                    Tasks = tasks
                };
            }
            return dto;
        }

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
