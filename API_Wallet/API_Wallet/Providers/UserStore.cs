using System;
using System.Threading.Tasks;
using API_Wallet.Models;
using API_Wallet.Repository;
using Microsoft.AspNet.Identity;

namespace API_Wallet.Providers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UserStore<T> : IUserStore<User>, IUserPasswordStore<User>
    {
        private UserRepository _userRepository = new UserRepository();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _userRepository = null;
        }

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Not Supported!</exception>
        public Task CreateAsync(User user)
        {
            throw new Exception("Not Supported!");
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Not Supported!</exception>
        public Task UpdateAsync(User user)
        {
            throw new Exception("Not Supported!");
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Not Supported!</exception>
        public Task DeleteAsync(User user)
        {
            throw new Exception("Not Supported!");
        }

        /// <summary>
        /// Finds the by identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public Task<User> FindByIdAsync(string userId)
        {
            return _userRepository.FindById(userId);
        }

        /// <summary>
        /// Finds the by name asynchronous.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public Task<User> FindByNameAsync(string userName)
        {
            return _userRepository.FindById(userName);
        }

        /// <summary>
        /// Sets the password hash asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <returns></returns>
        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.Password = passwordHash;
            return Task.FromResult(true);
        }

        /// <summary>
        /// Gets the password hash asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Task<string> GetPasswordHashAsync(User user)
        {
            var password = System.Text.Encoding.UTF8.GetBytes(user.Password);
            return Task.FromResult(Convert.ToBase64String(password));
        }

        /// <summary>
        /// Determines whether [has password asynchronous] [the specified user].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(true);
        }
    }
}