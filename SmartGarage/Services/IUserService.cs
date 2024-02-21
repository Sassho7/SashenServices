using SmartGarage.DTOs;
using SmartGarage.Models.DTOs;
using SmartGarage.Models.DTOs.UserDTO;
using SmartGarage.Models.QueryParameters;
using SmartGarage.ViewModels;
using SmartGarage.Models;

namespace SmartGarage.Services
{
    public interface IUserService
    {
        UserResponseDTO Create(UserRegistrationDTO newUser);
        IList<UserResponseDTO> GetAll();
        IList<UserResponseDTO> FilterBy(UserQueryParameters filterParameters);
        UserResponseDTO GetById(int id);
        UserResponseDTO GetByName(string username);
        UserResponseDTO Update(int id, UpdateUserRequestDTO newData, string username);
        void SetPassword(string email, string password);
        User Delete(int id, string username);
        string Login(UserLoginDTO user);
        int GetCount();
        bool UserExists(string username);
        bool EmailExists(string email);
        void IsCurrentUserEmployee(string username);
        bool VerifyPasswordHash(string password, User user);
        string CreateToken(User user);
        User Authenticate(string username);
        public User GetLoggedInUser();
    }
}
