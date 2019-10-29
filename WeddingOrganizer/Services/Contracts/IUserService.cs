using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAllUsers();
        Task<UserModel> GetUser(long id);
        Task UpdateUser(UserModel user);
        Task DeleteUser(long id);
        UserModel Authenticate(string email, string password);
        Task Register(RegisterModel model);
    }
}
