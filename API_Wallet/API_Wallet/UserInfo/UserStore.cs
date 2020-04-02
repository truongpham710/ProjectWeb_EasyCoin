using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace API_Wallet.UserInfo
{
    /// <summary>
    /// Class that implements the key ASP.NET Identity user store iterfaces
    /// </summary>
    public class UserStore<TUser> : IUserStore<TUser>
              where TUser : IdentityUser
    {
 
        private readonly UserRolesTable _userRolesTable;
        private readonly UserLoginsTable _userLoginsTable;

        public IQueryable<TUser> Users
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        /// <summary>
        /// Default constructor that initializes a new LINQ to SQL Database instance using the Default Connection string
        /// </summary>
        public UserStore()
        {          
            _userRolesTable = new UserRolesTable();        
            _userLoginsTable = new UserLoginsTable();
        }

        /// <summary>
        /// Insert a new TUser in the UserTable
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task CreateAsync(TUser user)
        {
            //if (user == null)
            //{
            //    throw new ArgumentNullException("user");
            //}

            //_userTable.Insert(user);

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Returns an TUser instance based on a userId query 
        /// </summary>
        /// <param name="userId">The user's Id</param>
        /// <returns></returns>
        public Task<TUser> FindByIdAsync(string userId)
        {
            //if (string.IsNullOrEmpty(userId))
            //{
            //    throw new ArgumentException("Null or empty argument: userId");
            //}

            //var result = _userTable.GetUserById(userId);
            //return Task.FromResult(result);
            return null;
        }

        /// <summary>
        /// Returns an TUser instance based on a userName query 
        /// </summary>
        /// <param name="userName">The user's name</param>
        /// <returns></returns>
        //public Task<TUser> FindByNameAsync(string userName)
        //{
        //    //if (string.IsNullOrEmpty(userName))
        //    //{
        //    //    throw new ArgumentException("Null or empty argument: userName");
        //    //}
        //    //////testing skipped
        //    //if(userName.Equals(UserConstant.PhoneRegistrationEmail))
        //    //{
        //    //    return Task.FromResult<TUser>(null);
        //    //}

        //    //var result = _userTable.GetUserByName(userName);

        //    //// Should I throw if > 1 user?
        //    //if (result != null && result.Count == 1)
        //    //{
        //    //    return Task.FromResult(result[0]);
        //    //}

        //    //return Task.FromResult<TUser>(null);
        //}

        /// <summary>
        /// Updates the UsersTable with the TUser instance values
        /// </summary>
        /// <param name="user">TUser to be updated</param>
        /// <returns></returns>
        public Task UpdateAsync(TUser user)
        {
            //if (user == null)
            //{
            //    throw new ArgumentNullException(nameof(user));
            //}

            //_userTable.Update(user);

            //return Task.FromResult<object>(null);
            return null;
        }

        public void Dispose()
        {
        }

        /// <summary>
        /// Inserts a claim to the UserClaimsTable for the given user
        /// </summary>
        /// <param name="user">User to have claim added</param>
        /// <param name="claim">Claim to be added</param>
        /// <returns></returns>
        public Task AddClaimAsync(TUser user, Claim claim)
        {
            //if (user == null)
            //{
            //    throw new ArgumentNullException(nameof(user));
            //}

            //if (claim == null)
            //{
            //    throw new ArgumentNullException(nameof(user));
            //}

            //_userClaimsTable.Insert(claim, user.Id);

            //return Task.FromResult<object>(null);
            return null;
        }

        /// <summary>
        /// Returns all claims for a given user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            return null;
            //ClaimsIdentity identity = _userClaimsTable.FindByUserId(user.Id);

            //return Task.FromResult<IList<Claim>>(identity.Claims.ToList());
        }

        /// <summary>
        /// Removes a claim froma user
        /// </summary>
        /// <param name="user">User to have claim removed</param>
        /// <param name="claim">Claim to be removed</param>
        /// <returns></returns>
        public Task RemoveClaimAsync(TUser user, Claim claim)
        {
            //if (user == null)
            //{
            //    throw new ArgumentNullException(nameof(user));
            //}

            //if (claim == null)
            //{
            //    throw new ArgumentNullException(nameof(claim));
            //}

            //_userClaimsTable.Delete(user, claim);

            //return Task.FromResult<object>(null);
            return null;
        }

        /// <summary>
        /// Inserts a Login in the UserLoginsTable for a given User
        /// </summary>
        /// <param name="user">User to have login added</param>
        /// <param name="login">Login to be added</param>
        /// <returns></returns>
        //public Task AddLoginAsync(TUser user, UserLoginInfo login)
        //{
        //    if (user == null)
        //    {
        //        throw new ArgumentNullException(nameof(user));
        //    }

        //    if (login == null)
        //    {
        //        throw new ArgumentNullException(nameof(login));
        //    }

        //    _userLoginsTable.Insert(user, login);

        //    return Task.FromResult<object>(null);
        //}

        /// <summary>
        /// Returns an TUser based on the Login info
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        //public Task<TUser> FindAsync(UserLoginInfo login)
        //{
        //    if (login == null)
        //    {
        //        throw new ArgumentNullException(nameof(login));
        //    }

        //    var userId = _userLoginsTable.FindUserIdByLogin(login);
        //    if (userId == null) return Task.FromResult<TUser>(null);
        //    var user = _userTable.GetUserById(userId);
        //    return Task.FromResult(user);
        //}

        /// <summary>
        /// Returns list of UserLoginInfo for a given TUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        //{
        //    if (user == null)
        //    {
        //        throw new ArgumentNullException(nameof(user));
        //    }

        //    var logins = _userLoginsTable.FindByUserId(user.Id);
        //    return Task.FromResult<IList<UserLoginInfo>>(logins);
        //}

        /// <summary>
        /// Deletes a login from UserLoginsTable for a given TUser
        /// </summary>
        /// <param name="user">User to have login removed</param>
        /// <param name="login">Login to be removed</param>
        /// <returns></returns>
        public Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }

            //userLoginsTable.Delete(user, login); //no use delete function in Easybook4.0

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inserts a entry in the UserRoles table
        /// </summary>
        /// <param name="user">User to have role added</param>
        /// <param name="roleName">Name of the role to be added to user</param>
        /// <returns></returns>
        //public Task AddToRoleAsync(TUser user, string roleName)
        //{
        //    if (user == null)
        //    {
        //        throw new ArgumentNullException(nameof(user));
        //    }

        //    if (string.IsNullOrEmpty(roleName))
        //    {
        //        throw new ArgumentException("Argument cannot be null or empty: roleName.");
        //    }

        //    string roleId = _roleTable.GetRoleId(roleName);
        //    if (!string.IsNullOrEmpty(roleId))
        //    {
        //        _userRolesTable.Insert(user, roleId);
        //    }

        //    return Task.FromResult<object>(null);
        //}

        ///// <summary>
        ///// Returns the roles for a given TUser
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public Task<IList<string>> GetRolesAsync(TUser user)
        //{
        //    if (user == null)
        //    {
        //        throw new ArgumentNullException(nameof(user));
        //    }

        //    List<string> roles = _userRolesTable.FindByUserId(user.Id);
        //    {
        //        if (roles != null)
        //        {
        //            return Task.FromResult<IList<string>>(roles);
        //        }
        //    }

        //    return Task.FromResult<IList<string>>(null);
        //}

        ///// <summary>
        ///// Verifies if a user is in a role
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="role"></param>
        ///// <returns></returns>
        //public Task<bool> IsInRoleAsync(TUser user, string role)
        //{
        //    if (user == null)
        //    {
        //        throw new ArgumentNullException(nameof(user));
        //    }

        //    if (string.IsNullOrEmpty(role))
        //    {
        //        throw new ArgumentNullException(nameof(role));
        //    }

        //    var roles = _userRolesTable.FindByUserId(user.Id);
        //    {
        //        if (roles != null && roles.Contains(role))
        //        {
        //            return Task.FromResult<bool>(true);
        //        }
        //    }

        //    return Task.FromResult<bool>(false);
        //}

        ///// <summary>
        ///// Removes a user from a role
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="role"></param>
        ///// <returns></returns>
        //public Task RemoveFromRoleAsync(TUser user, string role)
        //{
        //    throw new NotImplementedException();
        //}

        ///// <summary>
        ///// Deletes a user
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public Task DeleteAsync(TUser user)
        //{
        //    if (user != null)
        //    {
        //        _userTable.Delete(user);
        //    }

        //    return Task.FromResult<object>(null);
        //}

        ///// <summary>
        ///// Returns the PasswordHash for a given TUser
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public Task<string> GetPasswordHashAsync(TUser user)
        //{
        //    string passwordHash = _userTable.GetPasswordHash(user.Id);

        //    return Task.FromResult(passwordHash);
        //}

        ///// <summary>
        ///// Verifies if user has password
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public Task<bool> HasPasswordAsync(TUser user)
        //{
        //    var hasPassword = !string.IsNullOrEmpty(_userTable.GetPasswordHash(user.Id));

        //    return Task.FromResult(bool.Parse(hasPassword.ToString()));
        //}

        ///// <summary>
        ///// Sets the password hash for a given TUser
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="passwordHash"></param>
        ///// <returns></returns>
        //public Task SetPasswordHashAsync(TUser user, string passwordHash)
        //{
        //    //insert step 1
        //    user.PasswordHash = passwordHash;
        //    return Task.FromResult<object>(_userTable.SetPasswordHash(user.Id, passwordHash));
        //}

        ///// <summary>
        /////  Set security stamp
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="stamp"></param>
        ///// <returns></returns>
        //public Task SetSecurityStampAsync(TUser user, string stamp)
        //{
        //    //insert step 2
        //    user.SecurityStamp = stamp;

        //    return Task.FromResult(0);
        //}

        ///// <summary>
        ///// Get security stamp
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public Task<string> GetSecurityStampAsync(TUser user)
        //{
        //    return Task.FromResult(user.SecurityStamp);
        //}

        ///// <summary>
        ///// Set email on user
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="email"></param>
        ///// <returns></returns>
        //public Task SetEmailAsync(TUser user, string email)
        //{
        //    user.Email = email;
        //    _userTable.Update(user);

        //    return Task.FromResult(0);
        //}

        ///// <summary>
        ///// Get email from user
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public Task<string> GetEmailAsync(TUser user)
        //{
        //    ////set null to string.empty
        //    //string email = string.Empty;
        //    //email = string.IsNullOrEmpty(user.Email) ? string.Empty : user.Email;

        //    //return Task.FromResult(email);
        //    return Task.FromResult(user.Email);
        //}

        ///// <summary>
        ///// Get if user email is confirmed
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public Task<bool> GetEmailConfirmedAsync(TUser user)
        //{
        //    return Task.FromResult(user.EmailConfirmed);
        //}

        ///// <summary>
        ///// Set when user email is confirmed
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="confirmed"></param>
        ///// <returns></returns>
        //public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        //{
        //    user.EmailConfirmed = confirmed;
        //    _userTable.Update(user);

        //    return Task.FromResult(0);
        //}

        ///// <summary>
        ///// Get user by email
        ///// </summary>
        ///// <param name="email"></param>
        ///// <returns></returns>
        //public Task<TUser> FindByEmailAsync(string email)
        //{
        //    if (string.IsNullOrEmpty(email))
        //    {
        //        throw new ArgumentNullException(nameof(email));
        //    }

        //    var result = _userTable.GetUserByEmail(email);

        //    // just return user to avoid error
        //    if (result != null && result.Count > 0)
        //    {
        //        return Task.FromResult(result[0]);
        //    }

        //    return Task.FromResult<TUser>(null);
        //}

        ///// <summary>
        ///// Set user phone number
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="phoneNumber"></param>
        ///// <returns></returns>
        //public Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        //{
        //    user.PhoneNumber = phoneNumber;
        //    _userTable.Update(user);

        //    return Task.FromResult(0);
        //}

        ///// <summary>
        ///// Get user phone number
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public Task<string> GetPhoneNumberAsync(TUser user)
        //{
        //    return Task.FromResult(user.PhoneNumber);
        //}

        ///// <summary>
        ///// Get if user phone number is confirmed
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        //{
        //    return Task.FromResult(user.PhoneNumberConfirmed);
        //}

        ///// <summary>
        ///// Set phone number if confirmed
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="confirmed"></param>
        ///// <returns></returns>
        //public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        //{
        //    user.PhoneNumberConfirmed = confirmed;
        //    _userTable.Update(user);

        //    return Task.FromResult(0);
        //}

        ///// <summary>
        ///// Set two factor authentication is enabled on the user
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="enabled"></param>
        ///// <returns></returns>
        //public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        //{
        //    user.TwoFactorEnabled = enabled;
        //    _userTable.Update(user);

        //    return Task.FromResult(0);
        //}

        ///// <summary>
        ///// Get if two factor authentication is enabled on the user
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public Task<bool> GetTwoFactorEnabledAsync(TUser user)
        //{
        //    return Task.FromResult(user.TwoFactorEnabled);
        //}

        ///// <summary>
        ///// Get user lock out end date
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        //{
        //    return
        //        Task.FromResult(user.LockoutEndDateUtc.HasValue
        //            ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc))
        //            : new DateTimeOffset());
        //}


        ///// <summary>
        ///// Set user lockout end date
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="lockoutEnd"></param>
        ///// <returns></returns>
        //public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        //{
        //    user.LockoutEndDateUtc = lockoutEnd.UtcDateTime;
        //    _userTable.Update(user);

        //    return Task.FromResult(0);
        //}

        ///// <summary>
        ///// Increment failed access count
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public Task<int> IncrementAccessFailedCountAsync(TUser user)
        //{
        //    user.AccessFailedCount++;
        //    _userTable.Update(user);

        //    return Task.FromResult(user.AccessFailedCount);
        //}

        ///// <summary>
        ///// Reset failed access count
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public Task ResetAccessFailedCountAsync(TUser user)
        //{
        //    user.AccessFailedCount = 0;
        //    _userTable.Update(user);

        //    return Task.FromResult(0);
        //}

        ///// <summary>
        ///// Get failed access count
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public Task<int> GetAccessFailedCountAsync(TUser user)
        //{
        //    return Task.FromResult(user.AccessFailedCount);
        //}

        ///// <summary>
        ///// Get if lockout is enabled for the user
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public Task<bool> GetLockoutEnabledAsync(TUser user)
        //{
        //    return Task.FromResult(user.LockoutEnabled);
        //}

        ///// <summary>
        ///// Set lockout enabled for user
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="enabled"></param>
        ///// <returns></returns>
        //public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        //{
        //    user.LockoutEnabled = enabled;
        //    _userTable.Update(user);

        //    return Task.FromResult(0);
        //}


        ///// <summary>
        ///// Testing FindByPhone.
        ///// All Dummy Function From FindAsync.
        ///// This is not default Identity Interface Method.
        ///// </summary>
        ///// <param name="phoneNumber"></param>
        ///// <returns></returns>
        //public Task<TUser> FindByPhone(string phoneNumber, string countryCode)
        //{
        //    if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(countryCode))
        //    {
        //        throw new ArgumentException("Null or empty argument: phoneNumber / countryCode");
        //    }

        //    List<TUser> result = _userTable.GetUserByPhoneNumber(phoneNumber, countryCode) as List<TUser>;

        //    // Should I throw if > 1 user?
        //    if (result != null && result.Count == 1)
        //    {
        //        return Task.FromResult<TUser>(result[0]);
        //    }

        //    return Task.FromResult<TUser>(null);
        //}
    }
}