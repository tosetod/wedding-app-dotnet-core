using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAllUsers();
        UserModel GetUser(long id);
        void UpdateUser(UserModel user);
        void DeleteUser(long id);
        UserModel Authenticate(string email, string password);
        void Register(RegisterModel model);
    }
}
