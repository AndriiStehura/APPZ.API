using NUnit.Framework;
using APPZ.DAL.Entities;
using System.Collections.Generic;
using APPZ.DAL;
using System.Threading.Tasks;
using APPZ.BLL.Services;
using APPZ.BLL.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using APPZ.DAL.DTO;
using System;

namespace APPZ.Test
{
    public class BLLTest
    {
        private APPZDBContext _context;
        private IUnitOfWork _unitOfWork;
        private IUserService _userService;
        private IAuthService _authService;
        private ITaskService _taskService;

        public BLLTest()
        {
            var options = new DbContextOptionsBuilder<APPZDBContext>()
            .UseInMemoryDatabase(databaseName: "UsersDB")
            .Options;
            _context = new APPZDBContext(options);
            _context.AddRange(GetUsers());
            _context.AddRange(GetIdentities());
            _context.AddRange(GetAdminRecords());
            _context.AddRange(GetThemes());
            _context.AddRange(GetTasks());
            _context.SaveChanges();
            _unitOfWork = new UnitOfWork(_context);
            _userService = new UserService(_unitOfWork);
            _authService = new AuthService(_unitOfWork);
            _taskService = new TaskService(_unitOfWork);
        }

        private List<User> GetUsers() => new List<User>()
        {
            new User()
            {
                Email = "a@mail.com",
                FirstName = "A",
                LastName = "B",
                Group = "1",
                IdentityId = 1
            },
            new User()
            {
                Email = "E@mail.com",
                FirstName = "E",
                LastName = "F",
                Group = "2",
                IdentityId = 2
            },
            new User()
            {
                Email = "b@mail.com",
                FirstName = "B",
                LastName = "C",
                Group = "1",
                IdentityId = 3
            },
            new User()
            {
                Email = "c@mail.com",
                FirstName = "C",
                LastName = "D",
                Group = "1",
                IdentityId = 4
            },
            new User()
            {
                Email = "d@mail.com",
                FirstName = "D",
                LastName = "E",
                Group = "2",
                IdentityId = 5
            },
        };

        private List<Identity> GetIdentities() => new List<Identity>()
        {
            new Identity
            {
                PasswordHash = "password1"
            },
            new Identity
            {
                PasswordHash = "password2"
            },
            new Identity
            {
                PasswordHash = "password3"
            },
            new Identity
            {
                PasswordHash = "password4"
            },
            new Identity
            {
                PasswordHash = "password5"
            }
        };

        private List<AdminRecord> GetAdminRecords() => new List<AdminRecord>
        {
            new AdminRecord { UserId = 1},
            new AdminRecord { UserId = 4},
        };

        private List<TaskTheme> GetThemes() => new List<TaskTheme>
        {
            new TaskTheme
            {
                Name = "Lol kek"
            }
        };

        private List<LabTask> GetTasks() => new List<LabTask>
        {
            new LabTask
            {
                Answer = "andrii",
                ComplexityLevel = DAL.Enums.Complexity.Low,
                Description = "Call me",
                ThemeId = 1
            }
        };

        [Test, Order(1)]
        public async Task GetAllTest()
        {
            var users = await _userService.GetAllUsersAsync();
            Assert.IsNotEmpty(users);
            Assert.AreEqual(users.Count(), GetUsers().Count);
        }

        [Test]
        public async Task GetByIdPositiveTest()
        {
            Assert.IsNotNull(await _userService.GetUserByIdAsync(1));
        }

        [Test]
        public async Task GetByIdNegativeTest()
        {
            Assert.IsNull(await _userService.GetUserByIdAsync(int.MaxValue));
        }

        [Test]
        public async Task AddUserTest()
        {
            var newUser = new User()
            {
                Email = "f@email",
                FirstName = "F",
                Group = "3",
                LastName = "G"
            };
            Assert.AreEqual(newUser.Id, 0);
            await _userService.AddUserAsync(newUser);
            Assert.AreNotEqual(newUser.Id, 0);
        }

        [Test]
        public async Task UpdateUserTest()
        {
            var user = await _userService.GetUserByIdAsync(1);
            var userDto = new UpdateUserDTO 
            { 
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Group = "PZ",
                Id = user.Id
            };
            await _userService.UpdateUserAsync(userDto);
            var updatedUser = await _userService.GetUserByIdAsync(1);
            Assert.AreEqual(userDto.Group, updatedUser.Group);
        }

        [Test]
        public async Task AuthPositiveTest()
        {
            var authDTO = new AuthDTO
            {
                Email = "a@mail.com",
                PasswordHash = "password1"
            };
            var result = await _authService.Authentificate(authDTO);
            Assert.NotNull(result);
        }

        [Test]
        public async Task AuthNegativeTest()
        {
            var authDTO1 = new AuthDTO
            {
                Email = "a1@mail.com",
                PasswordHash = "password1"
            };
            var result1 = await _authService.Authentificate(authDTO1);
            Assert.IsNull(result1);

            var authDTO2 = new AuthDTO
            {
                Email = "a@mail.com",
                PasswordHash = "password11"
            };
            var result2 = await _authService.Authentificate(authDTO2);
            Assert.IsNull(result2);

            var authDTO3 = new AuthDTO
            {
                Email = "a1@mail.com",
                PasswordHash = "password11"
            };

            var result3 = await _authService.Authentificate(authDTO3);
            Assert.IsNull(result3);
        }

        [Test] 
        public async Task IsAdminPositiveTest()
        {
            Assert.IsTrue(await _authService.IsAdminAsync("a@mail.com"));
        }

        [Test]
        public async Task IsAdminNegativeTest()
        {
            Assert.IsFalse(await _authService.IsAdminAsync("b@mail.com"));
        }
        
        [Test, Order(2)]
        public async Task RegisterPositiveTest()
        {
            var newUser = new User()
            {
                Email = "g@mail.com",
                Group = "4",
                Identity = new Identity { PasswordHash = "passwordg" },
                FirstName = "G",
                LastName = "H"
            };
            var result = await _authService.Register(newUser);
            Assert.NotNull(result);
        }

        [Test, Order(3)]
        public async Task RegisterNegativeTest()
        {
            var newUser = new User()
            {
                Email = "g@mail.com",
                Group = "4",
                Identity = new Identity { PasswordHash = "passwordg" },
                FirstName = "G",
                LastName = "H"
            };
            try
            {
                await _authService.Register(newUser);
            }
            catch(ArgumentException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task CheckAnswerTest()
        {
            var answer = new AnswerDTO 
            {
                Answer = "andrii",
                TaskId = 1,
                UserId = 1
            };

            var result = await _taskService.EvaluateAnswer(answer);
            Assert.AreEqual(100, result.RightInPercent);

            answer.Answer = "ndrii";
            result = await _taskService.EvaluateAnswer(answer);
            Assert.AreNotEqual(100, result.RightInPercent);
        }
    }
}