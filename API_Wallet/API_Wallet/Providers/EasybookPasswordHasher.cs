using System;
using System.Threading.Tasks;
using API_Wallet.Models;
using Microsoft.AspNet.Identity;

namespace API_Wallet.Providers
{
    /// <summary> 
    /// Use Custom approach to verify password 
    /// </summary> 
    public class EasybookPasswordHasher : PasswordHasher
    {
        /// <summary>
        /// Hash a password
        /// </summary>
        /// <param name="password"/>
        /// <returns/>
        public override string HashPassword(string password)
        {
//            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
            return base.HashPassword(password);
        }

        /// <summary>
        /// Verify that a password matches the hashedPassword
        /// </summary>
        /// <param name="hashedPassword"/><param name="providedPassword"/>
        /// <returns/>
        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            return base.VerifyHashedPassword(hashedPassword, providedPassword);
//            var password = HashPassword(providedPassword);
//            return password == hashedPassword ? PasswordVerificationResult.SuccessRehashNeeded : PasswordVerificationResult.Failed;

        }
    }

}

