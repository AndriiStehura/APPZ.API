using APPZ.DAL.Entities;
using APPZ.DAL.Repositories;
using System;
using System.Threading.Tasks;

namespace APPZ.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly APPZDBContext _context;

        IGenericRepository<AdminRecord, int> _admin;
        IGenericRepository<Identity, int> _identity;
        IGenericRepository<LabTask, int> _tasks;
        IGenericRepository<TaskStatistics, int> _statistics;
        IGenericRepository<TaskTheme, int> _themes;
        IGenericRepository<User, int> _users;

        public UnitOfWork(APPZDBContext context)
        {
            _context = context;
        }

        public IGenericRepository<AdminRecord, int> AdminsRepository => 
            _admin ??= new GenericRepository<AdminRecord, int>(_context);

        public IGenericRepository<Identity, int> IdentityRepository =>
            _identity ??= new GenericRepository<Identity, int>(_context);

        public IGenericRepository<LabTask, int> TasksRepository =>
            _tasks ??= new GenericRepository<LabTask, int>(_context);

        public IGenericRepository<TaskStatistics, int> StatisticsRepository =>
            _statistics ??= new GenericRepository<TaskStatistics, int>(_context);

        public IGenericRepository<TaskTheme, int> ThemesRepository =>
            _themes ??= new GenericRepository<TaskTheme, int>(_context);

        public IGenericRepository<User, int> UsersRepository =>
            _users ??= new GenericRepository<User, int>(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
