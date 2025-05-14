using BackendApi.Users.DTOs;
namespace BackendApi.Users.Services
{
    public interface IUserService
    {
        public List<string> Errors { get; }
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> GetById(int id);
        Task<UserDto> Create(UserInsertDto userInsertDto);


        bool Validate(UserInsertDto dto);
    }
}