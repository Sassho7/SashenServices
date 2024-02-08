using SmartGarage.Models;
using SmartGarage.Models.DTOs;

namespace SmartGarage.Repositories;

public interface IUserRepository
{
    public List<User> GetUsers(string firstName, int phone);
    public List<User> GetByPhone(int phone);
    //public User GetByPhoneNumber(int phone);
    public User GetByExactName(string firstName);
    public List <User> GetByEmail(string email);
    // metod za registrirane na user
    // metod za redaktirane na user
}