
using Microsoft.EntityFrameworkCore;
using SmartGarage.Data;
using SmartGarage.Exceptions;
using SmartGarage.Models;
using SmartGarage.Repositories;
using System;
using SmartGarage.DTOs;
using SmartGarage.Models.QueryParameters;
namespace SmartGarage.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SGDbContext context;

        public UserRepository(SGDbContext context)
        {
            this.context = context;
        }

        public IList<User> GetAll()
        {
            return context.Users.Where(u => !u.IsDeleted).ToList();
        }

        public IList<User> FilterBy(UserQueryParameters usersParams)
        {

            IQueryable<User> result = context.Users
               .Where(u => !u.IsDeleted);


            if (!string.IsNullOrEmpty(usersParams.Username))
            {
                result = result.Where(u => u.Username.Contains(usersParams.Username));
            }

            if (!string.IsNullOrEmpty(usersParams.Email))
            {
                result = result.Where(u => u.Email.Contains(usersParams.Email));
            }

            if (!string.IsNullOrEmpty(usersParams.PhoneNumber))
            {
                result = result.Where(u => u.PhoneNumber.Contains(usersParams.PhoneNumber));
            }

            return result.ToList();
        }


        public User GetById(int id)
        {
            return context.Users.FirstOrDefault(u => u.Id == id && !u.IsDeleted) ??
                throw new EntityNotFoundException($"User with id:\"{id}\" not found.");
        }

        public User GetByUsername(string name)
        {
            return context.Users.FirstOrDefault(u => u.Username == name && !u.IsDeleted) ??
               throw new EntityNotFoundException($"User with username:\"{name}\" is not found.");
        }

        public User GetByEmail(string email)
        {
            return context.Users.FirstOrDefault(u => u.Email == email && !u.IsDeleted) ??
               throw new EntityNotFoundException($"User with email:\"{email}\" is not found.");
        }

        public User Create(User newUser)
        {
            var username = newUser.Username;
            var email = newUser.Email;
            var phoneNumber = newUser.PhoneNumber;

            if (UserExists(username))
                throw new DuplicateEntityExcetion($"Username \"{username}\" already exists!");

            if (EmailExists(email))
                throw new DuplicateEntityExcetion($"Email \"{email}\" already exists!");

            if (PhoneNumberExists(phoneNumber))
                throw new DuplicateEntityExcetion($"PhoneNumber \"{phoneNumber}\" already exists!");

            newUser.JoinDate = DateTime.Now;
            context.Users.Add(newUser);
            context.SaveChanges();
            return newUser;
        }

        public User Update(int id, User updatedUser)
        {
            var newUser = context.Users.FirstOrDefault(u => u.Id == id && !u.IsDeleted) ??
                throw new EntityNotFoundException($"User with id:\"{id}\" not found.");

            newUser.Username = updatedUser.Username;
            newUser.Email = updatedUser.Email;
            newUser.PhoneNumber = updatedUser.PhoneNumber;
            newUser.IsEmployee = updatedUser.IsEmployee;

            context.SaveChanges();
            return newUser;
        }

        public User SetPassword(string email, string newPassword)
        {
            var userToUpdate = GetByEmail(email);
            userToUpdate.PasswordHash = newPassword;

            context.SaveChanges();
            return userToUpdate;
        }

        public User Delete(int id)
        {
            User toDelete = GetById(id);
            toDelete.IsDeleted = true;
            context.SaveChanges();
            return toDelete;
        }

        public bool UserExists(string username)
        {
            return context.Users.Any(u => u.Username == username && !u.IsDeleted);
        }

        public bool EmailExists(string email)
        {
            return context.Users.Any(u => u.Email == email && !u.IsDeleted);
        }

        public bool PhoneNumberExists(string phoneNumber)
        {
            return context.Users.Any(u => u.PhoneNumber == phoneNumber && !u.IsDeleted);
        }

        public int Count()
        {
            return context.Users.Where(u => !u.IsDeleted).Count();
        }


    }
}
