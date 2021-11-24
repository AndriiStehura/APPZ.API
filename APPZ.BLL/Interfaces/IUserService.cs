using APPZ.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APPZ.BLL.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);
        Task UpdateUserAsync(User user);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
