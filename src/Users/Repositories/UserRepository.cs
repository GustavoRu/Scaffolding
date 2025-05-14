using Microsoft.EntityFrameworkCore;
using BackendApi.Users.Models;
using BackendApi.Users.DTOs;
using BackendApi.Data;

namespace BackendApi.Users.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserModel>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<UserModel> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task Create(UserModel user)
        {

            await _context.Users.AddAsync(user);
            
        }



        public async Task Save() => await _context.SaveChangesAsync();
    }

}