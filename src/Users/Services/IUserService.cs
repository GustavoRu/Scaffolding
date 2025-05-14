using BackendApi.Users.DTOs;
namespace BackendApi.Users.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> GetById(int id);
        Task<UserDto> Create(UserInsertDto userInsertDto);


        bool Validate(UserInsertDto dto);
    }
}