using APPZ.DAL.DTO;
using APPZ.DAL.Entities;
using System.Threading.Tasks;

namespace APPZ.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Authentificate(AuthDTO authDTO);
        Task Register(User user);
        Task<bool> IsAdminAsync(string email);
    }
}
