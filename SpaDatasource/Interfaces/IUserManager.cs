using SpaDatasource.Entitites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpaDatasource.Interfaces
{
    public interface IUserManager
    {
        Task<User> FindByNameAsync(string username);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task CreateAsync(User user, string password);
    }
}
