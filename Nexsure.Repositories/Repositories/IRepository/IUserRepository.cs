using Nexsure.Entities.Domain_Models.Model;

namespace Nexsure.DataBridge.Repositories.IRepository
{
    public interface IUserRepository
    {
        Task<bool> AddUserAsync(Users users);

        Task<bool> UpdateUserAsync(Users users);

        Task<bool> DeleteUserAsync(int userId);

        Task<Users> GetUserByIdAsync(int userId);

        Task<IEnumerable<Users>> GetAllUsersAsync();
    }
}