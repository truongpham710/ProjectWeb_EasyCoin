using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using Easybook.Api.Core.Model.EasyWallet.Models;

namespace Easybook.Api.DataAccessLayer.QueryBuilder.Wallet
{
    /// <summary>
    /// 
    /// </summary>
    public class UserCardQueryBuilder : QueryBuilder<User_Card>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserCardQueryBuilder"/> class.
        /// </summary>
        public UserCardQueryBuilder(DbContext dbContext) : base(dbContext) { } 
    

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="userid">The identifier.</param>
        /// <returns></returns>
        public UserCardQueryBuilder HasUserIdnCurrency(string userid, string currencyCode)
        {
            Query = Query.Where(user => user.User_ID == userid && user.Currency_Code == currencyCode);
            return this;
        }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="userid">The identifier.</param>
        /// <returns></returns>
        public UserCardQueryBuilder HasUserId(string userid)
        {
            Query = Query.Where(user => user.User_ID == userid);
            return this;
        }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="CardID">The identifier.</param>
        /// <returns></returns>
        public UserCardQueryBuilder HasCardId(string CardID)
        {
            Query = Query.Where(user => user.ID == CardID);
            return this;
        }

        public UserCardQueryBuilder HasUserAndId(string userid, string Id)
        {
            Query = Query.Where(user => user.User_ID == userid && user.ID == Id);
            return this;
        }
    }
}
