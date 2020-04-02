using System.Threading.Tasks;
using API_Wallet.Models;
using Microsoft.AspNet.Identity;

namespace API_Wallet.Providers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EasybookUserManager<T> : UserManager<User>
    {
        /// <summary>
        /// 
        /// </summary>
        public EasybookUserManager() : base(new UserStore<User>())
        {
            //We can retrieve Old System Hash Password and can encypt or decrypt old password using custom approach. 
            //When we want to reuse old system password as it would be difficult for all users to initiate pwd change as per Idnetity Core hashing. 
            PasswordHasher = new EasybookPasswordHasher();
        }

        /// <summary>
        /// Return a user with the specified username and password or null if there is no match.
        /// </summary>
        /// <param name="userName"/><param name="password"/>
        /// <returns/>
        public override Task<User> FindAsync(string userName, string password)
        {
            Task<User> taskInvoke = Task<User>.Factory.StartNew(() =>
            {
                var user = Task.Run(() => FindByNameAsync(userName)).Result;

                if (user == null) { return null; }

                var verificationResults = PasswordHasher.VerifyHashedPassword(user.Password, password);

                if (verificationResults != PasswordVerificationResult.SuccessRehashNeeded &&
                    verificationResults != PasswordVerificationResult.Success)
                {
                    return null;
                }

                return new User
                {
                    UserName = userName,
                    Id = user.Id,
                    Password = user.Password,
                    Role = user.Role,
                    Permission = user.Permission,
                    ApiSecretkey = user.ApiSecretkey
                };
            });

            return taskInvoke;
        }
    }
}