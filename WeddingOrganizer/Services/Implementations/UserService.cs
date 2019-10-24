using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using AutoMapper;
using DataAccess.Contracts;
using DataModels.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using Services.Contracts;
using Services.Exceptions;

namespace Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public UserService(IRepository<User> userRepository, IMapper mapper, IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }
        public IEnumerable<UserModel> GetAllUsers()
        {
            return _mapper.Map<IEnumerable<UserModel>>(_userRepository.GetAll());
        }

        public UserModel GetUser(long id)
        {
            var restaurant = _userRepository.GetById(id);
            return _mapper.Map<UserModel>(restaurant ?? throw new ResourceNotFoundException<User>(id));
        }

        public void UpdateUser(UserModel user)
        {
            _userRepository.Update(
                _mapper.Map<User>(user));
        }

        public void DeleteUser(long id)
        {
            var user = GetUser(id);
            _userRepository.Delete(
                _mapper.Map<User>(user?? throw new ResourceNotFoundException<User>(id)));
        }

        public UserModel Authenticate(string email, string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            var hashedPassword = Encoding.ASCII.GetString(md5Data);

            var user = _userRepository.GetAll().SingleOrDefault(x => x.Email == email && x.Password == hashedPassword);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userModel = _mapper.Map<UserModel>(user);
            userModel.Token = tokenHandler.WriteToken(token);

            return userModel;
        }

        public void Register(RegisterModel model)
        {
            //if(string.IsNullOrEmpty(model.FirstName))
            //    throw new ToDoException("First name is required");

            //if (string.IsNullOrEmpty(model.LastName))
            //    throw new ToDoException("Last name is required");

            //if (!ValidUsername(model.Username))
            //    throw new ToDoException("Username is already in use");

            //if (!ValidPassword(model.Password))
            //    throw new ToDoException("Please use stronger password");

            //if (model.Password != model.ConfirmPassword)
            //    throw new ToDoException("Password and Confirm Password are not matching");

            var md5 = new MD5CryptoServiceProvider();
            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(model.Password));
            var hashedPassword = Encoding.ASCII.GetString(md5Data);

            var user = _mapper.Map<User>(model);

            _userRepository.Add(user);
        }

        private static bool ValidPassword(string password)
        {
            var passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{6,20}$");
            var match = passwordRegex.Match(password);
            return match.Success;
        }

        private bool ValidUsername(string email)
        {
            return _userRepository.GetAll().All(x => x.Email != email);
        }
    }
}
