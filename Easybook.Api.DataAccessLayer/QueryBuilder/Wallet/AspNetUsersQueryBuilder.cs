using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using Easybook.Api.Core.Model.EasyWallet.Models;

namespace Easybook.Api.DataAccessLayer.QueryBuilder.Wallet
{
    /// <summary>
    /// 
    /// </summary>
    public class AspNetUsersQueryBuilder : QueryBuilder<AspNetUsers>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserQueryBuilder"/> class.
        /// </summary>
        public AspNetUsersQueryBuilder(DbContext dbContext) : base(dbContext) { } 
    

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="userid">The identifier.</param>
        /// <returns></returns>
        public AspNetUsersQueryBuilder HasUserId(string userid)
        {
            Query = Query.Where(user => user.Id == userid);
            return this;
        }

        /// <summary>
        /// Determines whether [has user name] [the specified user name].
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public AspNetUsersQueryBuilder HasUserName(string userName)
        {
            Query = Query.Where(user => user.UserName == userName);
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
        public AspNetUsersQueryBuilder GetEmail(string email)
        {
            Query = Query.Where(user => user.Email == email);
            return this;
        }

        /// <summary>
        /// Determines whether the specified user has phone.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns></returns>
        public AspNetUsersQueryBuilder GetPhoneNo(string phoneNumber)
        {
            Query = Query.Where(user => user.PhoneNumber == phoneNumber);
            return this;
        }

        /// <summary>
        /// Determines whether [has NIRC] [the specified NIRC].
        /// </summary>
        /// <param name="NRIC">Name of the NIRC.</param>
        /// <returns></returns>
        public AspNetUsersQueryBuilder GetNRIC(string NRIC)
        {
            Query = Query.Where(user => user.NRIC == NRIC);
            return this;
        }

        public AspNetUsersQueryBuilder HasIds(List<string> Ids)
        {
            Query = Query.Where(user => Ids.Contains(user.Id));
            return this;
        }
        public AspNetUsersQueryBuilder GetListBlockedUserID()
        {
            Query = Query.Where(user => user.UserName.Contains("blocked") && user.Status == 0);
            return this;
        }

    }
}
