using SpaDatasource.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using SpaDatasource.Entitites;
using System.Threading.Tasks;
using SpaDatasource.Helpers;

namespace SpaDatasource.Implementations
{
    public class SpaUserManager : IUserManager
    {
        private ISpaDatasource    _SpaDatasource;

        public SpaUserManager(ISpaDatasource spaDatasource)
        {
            _SpaDatasource = spaDatasource;
        }

        public Task<bool> CheckPasswordAsync(User user, string password)
        {
            bool isValid = PasswordHasher.Verify(password, user.PasswordHash);

            return Task.FromResult<bool>(isValid);
        }

        public Task<User> FindByNameAsync(string username)
        {
            return Task<User>.Run( () => 
            { 
                User u = null;

                try
                {
                    _SpaDatasource.Open();

                    u = _SpaDatasource.FindUserByName(username);
                }
                finally
                {
                    _SpaDatasource.Close();
                }

                return u;
            } );
        }

        public Task CreateAsync(User user, string password)
        {
            return Task<User>.Run( () => 
            { 
                try
                {
                    user.PasswordHash = PasswordHasher.Hash(password);

                    _SpaDatasource.Open();

                    _SpaDatasource.InsertUser(user);
                }
                finally
                {
                    _SpaDatasource.Close();
                }
            } );
        }
    }
}
