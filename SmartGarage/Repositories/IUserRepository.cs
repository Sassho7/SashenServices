using SmartGarage.DTOs;
using SmartGarage.Models;
using SmartGarage.Models.QueryParameters;
using System.Collections.Generic;

namespace SmartGarage.Repositories
{
    public interface IUserRepository
    {
        IList<User> GetAll();
        IList<User> FilterBy(UserQueryParameters usersParams);
        User GetById(int id);
        User GetByUsername(string name);
        User GetByEmail(string email);
        User Create(User newUser);
        User Update(int id, User updatedUser);
        User SetPassword(string email, string newPassword);
        User Delete(int id);
        bool UserExists(string username);
        bool EmailExists(string email);
        bool PhoneNumberExists(string phoneNumber);
        int Count();
    }
}
