using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using Easybook.Api.Core.Model.EasyWallet.Models;

namespace Easybook.Api.DataAccessLayer.QueryBuilder.Wallet
{
    /// <summary>
    /// 
    /// </summary>
    public class UserQueryBuilder : QueryBuilder<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserQueryBuilder"/> class.
        /// </summary>
        public UserQueryBuilder(DbContext dbContext) : base(dbContext) { } 
    

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="userid">The identifier.</param>
        /// <returns></returns>
        public UserQueryBuilder HasUserId(string userid)
        {
            Query = Query.Where(user => user.User_ID == userid);
            return this;
        }

        /// <summary>
        /// Determines whether [has user name] [the specified user name].
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public UserQueryBuilder HasUserName(string userName)
        {
            Query = Query.Where(user => user.User_Name == userName);
            return this;
        }

        /// <summary>
        /// Determines whether the specified user has email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public bool HasEmail(string email)
        {
            return Query.Any(user => user.Email == email);            
        }

        /// <summary>
        /// Determines whether the specified user has email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public UserQueryBuilder GetEmail(string email)
        {
            Query = Query.Where(user => user.Email == email);
            return this;
        }

        /// <summary>
        /// Determines whether the specified user has phone.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns></returns>
        public UserQueryBuilder HasPhone(string phoneNumber)
        {
            Query = Query.Where(user => user.PhoneNumber == phoneNumber);
            return this;
        }

        /// <summary>
        /// Determines whether [has NIRC] [the specified NIRC].
        /// </summary>
        /// <param name="NRIC">Name of the NIRC.</param>
        /// <returns></returns>
        public UserQueryBuilder HasNRIC(string NRIC)
        {
            Query = Query.Where(user => user.NRIC == NRIC);
            return this;
        }

        public UserQueryBuilder HasIds(List<string> Ids)
        {
            Query = Query.Where(user => Ids.Contains(user.User_ID));
            return this;
        }
    }
}
