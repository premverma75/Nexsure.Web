using Nexsure.DataBridge.Repositories.IRepository;
using Nexsure.Entities.Business_Model.Request_Model.User;
using Nexsure.Entities.Domain_Models.Model;
using Nexsure.Service.BusinessService.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexsure.Service.BusinessService.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateUserAsync(UserDto user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var usersEntity = new Users
            {
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                AddressId = user.AddressId?.AddressId ?? 0,
                DateOfBirth = user.DateOfBirth,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };

            return await _userRepository.AddUserAsync(usersEntity);
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            // Convert Guid to int (assuming mapping exists between UserDto.Id (Guid) and Users.Id (int))
            // This requires a lookup to get the Users entity by Guid, then use its int Id for deletion.
            // If such mapping is not available, this logic should be adjusted accordingly.

            // Example: Find the user by Guid (from UserDto), then delete by Users.Id (int)
            var allUsers = await _userRepository.GetAllUsersAsync();
            var userToDelete = allUsers.FirstOrDefault(u => u.Id.ToString() == userId.ToString());
            if (userToDelete == null)
                return false;

            return await _userRepository.DeleteUserAsync(userToDelete.Id);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(u => new UserDto
            {
                Id = Guid.TryParse(u.Id.ToString(), out var guid) ? guid : Guid.Empty,
                UserName = u.UserName,
                Password = u.Password,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                AddressId = u.Address != null ? new Address
                {
                    AddressId = u.Address.AddressId,
                    Street = u.Address.Street,
                    City = u.Address.City,
                    State = u.Address.State,
                    ZipCode = u.Address.ZipCode,
                    Country = u.Address.Country,
                    Province = u.Address.Province,
                    District = u.Address.District,
                    PostalCode = u.Address.PostalCode,
                    AddressLine1 = u.Address.AddressLine1,
                    AddressLine2 = u.Address.AddressLine2,
                    Landmark = u.Address.Landmark,
                    Region = u.Address.Region,
                    AddressType = u.Address.AddressType,
                    FullAddress = u.Address.FullAddress,
                    Subdistrict = u.Address.Subdistrict,
                    BuildingName = u.Address.BuildingName,
                    POBox = u.Address.POBox,
                    Latitude = u.Address.Latitude,
                    Longitude = u.Address.Longitude,
                    TimeZone = u.Address.TimeZone,
                    IsVerified = u.Address.IsVerified,
                    IsActive = u.Address.IsActive,
                    CreatedAt = u.Address.CreatedAt,
                    UpdatedAt = u.Address.UpdatedAt
                } : null,
                DateOfBirth = u.DateOfBirth,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt
            });
        }

        public async Task<UserDto> GetUserByIdAsync(Guid userId)
        {
            // Get all users and find the one matching the Guid
            var allUsers = await _userRepository.GetAllUsersAsync();
            var user = allUsers.FirstOrDefault(u => u.Id.ToString() == userId.ToString());
            if (user == null)
                return null;

            return new UserDto
            {
                Id = Guid.TryParse(user.Id.ToString(), out var guid) ? guid : Guid.Empty,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                AddressId = user.Address != null ? new Address
                {
                    AddressId = user.Address.AddressId,
                    Street = user.Address.Street,
                    City = user.Address.City,
                    State = user.Address.State,
                    ZipCode = user.Address.ZipCode,
                    Country = user.Address.Country,
                    Province = user.Address.Province,
                    District = user.Address.District,
                    PostalCode = user.Address.PostalCode,
                    AddressLine1 = user.Address.AddressLine1,
                    AddressLine2 = user.Address.AddressLine2,
                    Landmark = user.Address.Landmark,
                    Region = user.Address.Region,
                    AddressType = user.Address.AddressType,
                    FullAddress = user.Address.FullAddress,
                    Subdistrict = user.Address.Subdistrict,
                    BuildingName = user.Address.BuildingName,
                    POBox = user.Address.POBox,
                    Latitude = user.Address.Latitude,
                    Longitude = user.Address.Longitude,
                    TimeZone = user.Address.TimeZone,
                    IsVerified = user.Address.IsVerified,
                    IsActive = user.Address.IsActive,
                    CreatedAt = user.Address.CreatedAt,
                    UpdatedAt = user.Address.UpdatedAt
                } : null,
                DateOfBirth = user.DateOfBirth,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public async Task<bool> UpdateUserAsync(UserDto user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            // Find the Users entity by matching the Guid (UserDto.Id) to Users.Id (int)
            var allUsers = await _userRepository.GetAllUsersAsync();
            var userEntity = allUsers.FirstOrDefault(u => u.Id.ToString() == user.Id.ToString());
            if (userEntity == null)
                return false;

            // Update properties
            userEntity.UserName = user.UserName;
            userEntity.Password = user.Password;
            userEntity.Email = user.Email;
            userEntity.FirstName = user.FirstName;
            userEntity.LastName = user.LastName;
            userEntity.PhoneNumber = user.PhoneNumber;
            userEntity.AddressId = user.AddressId?.AddressId ?? 0;
            userEntity.DateOfBirth = user.DateOfBirth;
            userEntity.CreatedAt = user.CreatedAt;
            userEntity.UpdatedAt = user.UpdatedAt;

            return await _userRepository.UpdateUserAsync(userEntity);
        }

        public async Task<bool> ValidateUserCredentialsAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return false;

            var users = await _userRepository.GetAllUsersAsync();
            var user = users.FirstOrDefault(u => u.UserName == username && u.Password == password);
            return user != null;
        }
    }
}