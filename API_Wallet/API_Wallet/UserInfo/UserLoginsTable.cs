using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace API_Wallet.UserInfo
{
    /// <summary>
    /// Class that represents the UserLogins table in the Database
    /// </summary>
    public class UserLoginsTable
    {
        //private readonly IdentityUserLoginQueryBuilder _identityUserLoginQueryBuilder = new IdentityUserLoginQueryBuilder();
        //private readonly UserTransactionUow _userTransactionUow = new UserTransactionUow();

        /// <summary>
        /// Deletes a login from a user in the UserLogins.
        /// </summary>
        /// <param name="user">User to have login deleted</param>
        /// <param name="login">Login to be deleted from user</param>
        /// <returns>0</returns>
        public int Delete(IdentityUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
            //int returnValue = 0;

            //if (user != null && login != null)
            //{
            //    SingmayAdminService singmayAdminService = new SingmayAdminService();
            //    AspNetUserLogins _login = new AspNetUserLogins();

            //    // delete target login from target user in database
            //    returnValue = singmayAdminService.UserLogins_Delete(login.LoginProvider, login.ProviderKey, user.Id, common.getCallerID(), common.getCallerPWD());
            //}

            //return returnValue;
        }

        /// <summary>
        /// Deletes all Logins from a user in the UserLogins which match the given userId.
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns>0</returns>
        public int Delete(string userId)
        {
            throw new NotImplementedException();
            //int returnValue = 0;

            //if (!string.IsNullOrEmpty(userId))
            //{
            //    SingmayAdminService singmayAdminService = new SingmayAdminService();

            //    // delete all login from this user in database
            //    returnValue = singmayAdminService.UserLogins_DeleteAllFromUser(userId, common.getCallerID(), common.getCallerPWD());
            //}

            //return returnValue;
        }

        /// <summary>
        /// Inserts a new login in the UserLogins.
        /// </summary>
        /// <param name="user">User to have new login added</param>
        /// <param name="login">Login to be added</param>
        /// <returns>0</returns>
        public int Insert(IdentityUser user, UserLoginInfo login)
        {
            int returnValue = 0;

            if (user != null && login != null)
            {
                //SingmayAdminService singmayAdminService = new SingmayAdminService();

                //// insert into database
                //returnValue = singmayAdminService.InsertUserLogin(login.LoginProvider, login.ProviderKey, user.Id, common.getCallerID(), common.getCallerPWD());
                //using (var userTransactionUow = new UserTransactionUow())
                //{
                //    returnValue = userTransactionUow.InsertUserLogin(login.LoginProvider, login.ProviderKey, user.Id);
                //}
            }
            return returnValue;
        }

        /// <summary>
        /// Return a userId which match the given user's login.
        /// </summary>
        /// <param name="userLogin">The user's login info</param>
        /// <returns>The user's Id</returns>
        public string FindUserIdByLogin(UserLoginInfo userLogin)
        {
            string userId = null;

            if (userLogin == null) return null;

            //using(var identityUserLoginQueryBuilder = new IdentityUserLoginQueryBuilder())
            //{
            //    //SingmayAdminService singmayAdminService = new SingmayAdminService();

            //    //// get user id from database by giving login detail
            //    //userId = singmayAdminService.UserLogins_FindUserIdByLogin(userLogin.LoginProvider, userLogin.ProviderKey, common.getCallerID(), common.getCallerPWD());
            //    AspNetUserLogins userLogins = identityUserLoginQueryBuilder.HasLoginProvider(userLogin.LoginProvider).HasProviderKey(userLogin.ProviderKey).FirstOrDefault();

            //    if (userLogins != null) userId = userLogins.UserId;
            //}
            return userId;
        }

        /// <summary>
        /// Returns a list of user's logins which match the given userId.
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns>List of user's logins</returns>
        public List<UserLoginInfo> FindByUserId(string userId)
        {
            var logins = new List<UserLoginInfo>();
            if (string.IsNullOrEmpty(userId)) return logins;
            //SingmayAdminService singmayAdminService = new SingmayAdminService();

            // get all login detail from this user in database
            //DataSet resultDataSet = singmayAdminService.UserLogins_FindByUserId(userId, common.getCallerID(), common.getCallerPWD());
            //if (resultDataSet != null && resultDataSet.Tables != null && resultDataSet.Tables.Count > 0 && resultDataSet.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < resultDataSet.Tables[0].Rows.Count; ++i)
            //    {
            //        string loginProvider = resultDataSet.Tables[0].Rows[i]["LoginProvider"].ToString();
            //        string loginKey = resultDataSet.Tables[0].Rows[i]["ProviderKey"].ToString();

            //        UserLoginInfo login = new UserLoginInfo(loginProvider, loginKey);
            //        logins.Add(login);
            //    }
            //}
            //using (var identityUserLoginQueryBuilder = new IdentityUserLoginQueryBuilder())
            //{
            //    IEnumerable<AspNetUserLogins> userLogins = identityUserLoginQueryBuilder.HasUserId(userId).ToList();
            //    if (userLogins != null)
            //    {
            //        logins.AddRange(userLogins.Select(rowUserLogin => new UserLoginInfo(rowUserLogin.LoginProvider, rowUserLogin.ProviderKey)));
            //    }
            //}

            return logins;
        }
    }
}