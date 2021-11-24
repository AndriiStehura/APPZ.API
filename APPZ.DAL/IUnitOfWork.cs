using APPZ.DAL.Entities;
using APPZ.DAL.Repositories;
using System.Threading.Tasks;

namespace APPZ.DAL
{
    public interface IUnitOfWork
    {
        IGenericRepository<AdminRecord, int> AdminsRepository { get; }
        IGenericRepository<Identity, int> IdentityRepository { get; }
        IGenericRepository<LabTask, int> TasksRepository { get; }
        IGenericRepository<TaskStatistics, int> StatisticsRepository { get; }
        IGenericRepository<TaskTheme, int> ThemesRepository { get; }
        IGenericRepository<User, int> UsersRepository { get; }
        Task SaveAsync();
    }
}
