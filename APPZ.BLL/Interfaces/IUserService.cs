using APPZ.DAL.DTO;
using APPZ.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APPZ.BLL.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);
        Task UpdateUserAsync(UpdateUserDTO user);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task UpdatePasswordAsync(PasswordDTO passwordDTO);
    }
}
