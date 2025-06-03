using Microsoft.EntityFrameworkCore;
using Nexsure.DataBridge.DataContext;
using Nexsure.DataBridge.Repositories.IRepository;
using Nexsure.Entities.Domain_Models.Model;

namespace Nexsure.DataBridge.Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly NexsureAppDbContext _context;

        public UserRepository(NexsureAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddUserAsync(Users users)
        {
            _context.Set<Users>().Add(users);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Set<Users>().FindAsync(userId);
            if (user == null) return false;
            _context.Set<Users>().Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            return await _context.Set<Users>()
                .Include(u => u.Address)
                .ToListAsync();
        }

        public async Task<Users> GetUserByIdAsync(int userId)
        {
            return await _context.Set<Users>()
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> UpdateUserAsync(Users users)
        {
            _context.Set<Users>().Update(users);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}