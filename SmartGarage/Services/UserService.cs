using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Smart_Garage.Services.Contracts;
using SmartGarage.Exceptions;
using SmartGarage.Helpers;
using SmartGarage.Models.DTOs;
using SmartGarage.Models;
using SmartGarage.Models.DTOs.UserDTO;
using SmartGarage.Repositories;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using SmartGarage.Models.QueryParameters;
using SmartGarage.DTOs;
using SmartGarage.ViewModels;
using Microsoft.AspNetCore.Components.Forms;

namespace SmartGarage.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository usersRepository;
        private readonly IConfiguration configuration;
        private readonly IMapper autoMapper;
        private readonly IEmailSender emailSender;
        private readonly IPasswordHelper passwordHelper;
        public UserService(IUserRepository usersRepository, IConfiguration configuration, IMapper autoMapper, IEmailSender emailSender)
        {
            this.usersRepository = usersRepository;
            this.configuration = configuration;
            this.autoMapper = autoMapper;
            this.emailSender = emailSender;
            passwordHelper = new PasswordHelper();
        }

        public UserResponseDTO Create(UserRegistrationDTO newUser) 
        {
            if (usersRepository.UserExists(newUser.Username))
                throw new DuplicateEntityExcetion($"User with name {newUser.Username} already exists.");

            string randomPassword = passwordHelper.GenerateRandomPassword();

            User user = new User()
            {
                Username = newUser.Username,
                PasswordHash = randomPassword,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber

            };

            return autoMapper.Map<UserResponseDTO>(usersRepository.Create(user));
        }

        public IList<UserResponseDTO> GetAll()
        {
            var users = usersRepository.GetAll();
            return autoMapper.Map<IList<UserResponseDTO>>(users);
        }


        public IList<UserResponseDTO> FilterBy(UserQueryParameters filterParameters)
        {
            return usersRepository.FilterBy(filterParameters)
                            .Select(u => autoMapper.Map<UserResponseDTO>(u))
                            .ToList();
        }
        public UserResponseDTO GetById(int id) 
        { 
            User user = usersRepository.GetById(id);
            return autoMapper.Map<UserResponseDTO>(user);
        }

        public UserResponseDTO GetByName(string username)
        {
            User user = usersRepository.GetByUsername(username);
            return autoMapper.Map<UserResponseDTO>(user);
        }

        public UserResponseDTO Update(int id, UpdateUserRequestDTO newData, string username)
        {
            User user = new User();
            user.Username = newData.Username;
            user.Email = newData.Email;
            user.PhoneNumber = newData.PhoneNumber;
            user.IsEmployee = newData.IsAdmin;

            User updatedUser = usersRepository.Update(id, user);

            return autoMapper.Map<UserResponseDTO>(updatedUser);
        }

        public void SetPassword(string email, string password)
        {


            usersRepository.SetPassword(email, password);
        }

        public User Delete(int id, string username)
        {
            return usersRepository.Delete(id);
        }

        public string Login(UserLoginDTO user)
        {
            if (!usersRepository.UserExists(user.Username))
                throw new EntityNotFoundException("User not found.");

            User registeredUser = usersRepository.GetByUsername(user.Username);
            if (!VerifyPasswordHash( user.Password, registeredUser))
            {
                throw new AuthorizationException("Wrong password");
            }

            var token = CreateToken(registeredUser);
            return token;
        }

        public int GetCount()
        {
            return usersRepository.Count();
        }
        public bool UserExists(string username)
        {
            return usersRepository.UserExists(username);
        }

        public bool EmailExists(string email)
        {
            return usersRepository.EmailExists(email);
        }

        public void IsCurrentUserEmployee(string username) 
        {
            if (!usersRepository.GetByUsername(username).IsEmployee)
                throw new AuthorizationException("For employees only");
        }

        public async void SendEmailLogic(SendEmailViewModel userEmail)
        {
            string newPassword = passwordHelper.GenerateRandomPassword();
            SetPassword(userEmail.Email, newPassword);
            string username = usersRepository.GetByEmail(userEmail.Email).Username;
            var subject = "New password";
            var message = $"Hello, {username}, <br> Your new password is {newPassword}, you can change it once you log in on the platform using it, have a nice day!";
            await emailSender.SendEmailAsync(userEmail.Email, subject, message);
        }

        public bool VerifyPasswordHash(string password, User user)
        {
            if (password == user.PasswordHash)
            {
                return true;
            }
            return false;
        }
        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public User Authenticate(string username)
        {
            return usersRepository.GetByUsername(username);
        }
    }
}
