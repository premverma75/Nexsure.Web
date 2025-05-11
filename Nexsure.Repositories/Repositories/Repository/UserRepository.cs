using Nexsure.DataBridge.Repositories.IRepository;
using Nexsure.Entities.Domain_Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexsure.DataBridge.Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        public Task<bool> AddUserAsync(Users users)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Users> GetUserByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserAsync(Users users)
        {
            throw new NotImplementedException();
        }
    }
}
