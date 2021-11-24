using APPZ.BLL.Interfaces;
using APPZ.DAL;
using APPZ.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APPZ.BLL.Services
{
    public class UserService: IUserService
    {
        IUnitOfWork _unit;

        public UserService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task AddUserAsync(User user)
        {
            await _unit.UsersRepository.InsertAsync(user);
            await _unit.SaveAsync();
        }

        public async Task<User> GetUserByIdAsync(int id) => await _unit.UsersRepository.GetByIdAsync(id);

        public async Task UpdateUserAsync(User user)
        {
            _unit.UsersRepository.Update(user);
            await _unit.SaveAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync() => await _unit.UsersRepository.GetAsync();
    }
}
