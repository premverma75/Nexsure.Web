using Nexsure.Entities.Business_Model.Request_Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexsure.Service.BusinessService.IService
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(Guid userId);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<bool> CreateUserAsync(UserDto user);
        Task<bool> UpdateUserAsync(UserDto user);
        Task<bool> DeleteUserAsync(Guid userId);
        Task<bool> ValidateUserCredentialsAsync(string username, string password);
        
    }
}
