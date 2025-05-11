using Nexsure.Entities.Domain_Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
