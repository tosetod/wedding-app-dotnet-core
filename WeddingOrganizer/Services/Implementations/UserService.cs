using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
    public static class Roles
    {
        public const string User = "User";
    }

    public class UserService : IUserService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;
        private readonly IRepository<Restaurant> _restaurantRepository;
        private readonly IRepository<User> _userRepository;

        public UserService(
            IRepository<User> userRepository, 
            IMapper mapper, 
            IOptions<JwtSettings> jwtSettings,
            IRepository<Restaurant> restaurantRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// Authenticate user with JWT Bearer Token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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

        public async Task DeleteUser(long id)
        {
            var user = GetUser(id);
            if (user == null)
            {
                throw new ResourceNotFoundException<User>(id);
            }
            await _userRepository.Delete(_mapper.Map<User>(user));
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _mapper.Map<IEnumerable<UserModel>>(_userRepository.GetAll());
        }

        public async Task<UserModel> GetUser(long id)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
            {
                throw new ResourceNotFoundException<User>(id);
            }

            var restaurantId = user.RestaurantId;
            if (restaurantId.HasValue)
            {
                user.Restaurant = await _restaurantRepository.GetById(restaurantId.Value);
            }
            return _mapper.Map<UserModel>(user);
        }

        /// <summary>
        /// Register new user async
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task Register(RegisterModel model)
        {
            if (string.IsNullOrEmpty(model.FirstName))
                throw new UserException("First name is required");

            if (!ValidEmail(model.Email))
                throw new UserException("Account with this e-mail already exists");

            if (!ValidPassword(model.Password))
                throw new UserException("Please use stronger password");

            if (model.Password != model.ConfirmPassword)
                throw new UserException("Password and Confirm Password are not matching");

            var md5 = new MD5CryptoServiceProvider();
            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(model.Password));
            var hashedPassword = Encoding.ASCII.GetString(md5Data);

            var user = _mapper.Map<User>(model);
            user.Password = hashedPassword;

            await _userRepository.Add(user);
        }

        public async Task UpdateUser(UserModel user)
        {
            await _userRepository.Update(
                _mapper.Map<User>(user));
        }
        private static bool ValidPassword(string password)
        {
            var passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{6,20}$");
            var match = passwordRegex.Match(password);
            return match.Success;
        }

        private bool ValidEmail(string email)
        {
            return _userRepository.GetAll().All(x => x.Email != email);
        }
    }
}
