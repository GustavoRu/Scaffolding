using BackendApi.Users.Services;
using BackendApi.Users.DTOs;
using BackendApi.Users.Repositories;
using BackendApi.Users.Models;
namespace BackendApi.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public List<string> Errors { get; }

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            Errors = new List<string>();
        }
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return users.Select(u => new UserDto
            {
                UserId = u.UserId,
                Username = u.Username,
                Email = u.Email
            });
        }

        public async Task<UserDto> GetById(int id)
        {
            var user = await _userRepository.GetById(id);
            if(user != null)
            {
                return new UserDto
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Email = user.Email
                };
            }
            return null;
        }

        public async Task<UserDto> Create(UserInsertDto userInsertDto)
        {
            
            var user = new UserModel
            {
                Username = userInsertDto.Username,
                Email = userInsertDto.Email,
                PasswordHash = userInsertDto.Password,
            };

            
            await _userRepository.Create(user);
            await _userRepository.Save();

            return new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email
            };
        }



        public bool Validate(UserInsertDto userInsertDto)
        {
            if(_userRepository.Search(b => b.Email == userInsertDto.Email).Count() > 0){
                Errors.Add("No puede existir un usuario con el mismo email");
                return false;
            }
            return true;
        }
    }
}