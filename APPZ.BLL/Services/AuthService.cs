using APPZ.BLL.Interfaces;
using APPZ.DAL;
using APPZ.DAL.DTO;
using APPZ.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace APPZ.BLL.Services
{
    public class AuthService: IAuthService
    {
        readonly IUnitOfWork _unit;

        public AuthService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<UserDTO> Authentificate(AuthDTO authDTO)
        {
            var user = (await _unit.UsersRepository.GetAsync(include: x => x.Include(y => y.Identity)))
                .FirstOrDefault(x => string.Equals(x.Email, authDTO.Email, StringComparison.CurrentCultureIgnoreCase));
            if(user != null && user.Identity.PasswordHash == authDTO.PasswordHash)
            {
                return new UserDTO
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    Group = user.Group,
                    Id = user.Id,
                    LastName = user.LastName
                };
            }

            return null;
        }

        public async Task<UserDTO> Register(User user)
        {
            var users = await _unit.UsersRepository.GetAsync();
            if (users.Any(x => string.Equals(x.Email, user.Email, StringComparison.CurrentCultureIgnoreCase)))
            {
                throw new ArgumentException("User already exists");
            }

            await _unit.UsersRepository.InsertAsync(user);
            await _unit.SaveAsync();

            return new UserDTO
            {
                Email = user.Email,
                FirstName = user.FirstName,
                Group = user.Group,
                Id = user.Id,
                LastName = user.LastName
            };
        }

        public async Task<bool> IsAdminAsync(string email) =>
            (await _unit.AdminsRepository.GetAsync(include: x => x.Include(y => y.User)))
            .Any(x => string.Equals(x.User.Email, email, StringComparison.CurrentCultureIgnoreCase));
    }
}
