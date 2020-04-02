using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easybook.Api.DataAccessLayer.UnitOfWork;
using Easybook.Api.Core.Model.EasyWallet.Models;
using Easybook.Api.DataAccessLayer.QueryBuilder.Wallet;
using Easybook.Api.BusinessLogic.EasyWallet.Constant;
using Easybook.Api.BusinessLogic.EasyWallet.Utility;
using System.Reflection;
using Easybook.Api.Core.CrossCutting.Utility;
using static Easybook.Api.BusinessLogic.EasyWallet.Utility.SecurityLogic;

namespace Easybook.Api.BusinessLogic.EasyWallet
{
    public class WalletUserLogic
    {
        public WalletUserLogic(bool genStampKey = false)
        {
            if (genStampKey)
            {
                //localhost.EWallet_StampService stampService = new localhost.EWallet_StampService();
                var EncryptTokenEBW = SimpleAesUtil.Encrypt(EwalletConstant.TokenEBW);
                Globals.StampServerKey = "5tg8ENcfBwP2z8pWsI5lL8Hab0Tr9VZ5";
                //Globals.StampServerKey = stampService.Generate_Stamp_Key(EncryptTokenEBW);
            }            
        }
        public string InsertUserRecord(User record)
        {
            try
            {
                using (var eWalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities()))
                {
                    record.User_ID = SecurityLogic.GenerateKey(30);
                    eWalletTransactionUnitOfWork.BeginTransaction().DoInsert(record).EndTransaction();
                }
                return record.User_ID;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), record.User_ID, ex,""));
                return "";
            }
        }

        public bool InsertUserRecordForEBW(User record)
        {
            try
            {
                using (var eWalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities()))
                {                    
                    eWalletTransactionUnitOfWork.BeginTransaction().DoInsert(record).EndTransaction();
                }
                return true;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), record.User_ID, ex, ""));
                return false;
            }
        }

        public bool InsertUserCard(User_Card record)
        {
            try
            {
                using (var eWalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities()))
                {
                    eWalletTransactionUnitOfWork.BeginTransaction().DoInsert(record).EndTransaction();
                }
                return true;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), record.User_ID, ex, ""));
                return false;
            }
        }

        public bool DeleteUserCard(string IDCard)
        {
            try
            {
                using (var eWalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities()))
                {
                    eWalletTransactionUnitOfWork.BeginTransaction();
                    var CardUser = new UserCardQueryBuilder(new WalletEntities()).HasCardId(IDCard).FirstOrDefault();
                    eWalletTransactionUnitOfWork.DoDelete(CardUser);
                    eWalletTransactionUnitOfWork.EndTransaction();
                }
                return true;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), IDCard, ex, ""));
                return false;
            }
        }

        public User ConvertAspNetUserToUser(AspNetUsers objAspNetUser)
        {
            var user = new User();
            user.Address = objAspNetUser.Address;
            user.City = objAspNetUser.City;
            user.CreateDate = objAspNetUser.CreateDate;
            user.CreateUser = objAspNetUser.CreateUser;
            user.DOB = objAspNetUser.DOB;
            user.Email = objAspNetUser.Email;
            user.EmailConfirmed = objAspNetUser.EmailConfirmed;
            user.FirstName = objAspNetUser.FirstName;
            user.GUID = Guid.NewGuid().ToString();
            user.LastName = objAspNetUser.LastName;
            user.Nationality = objAspNetUser.CountryId;
            user.NRIC = objAspNetUser.NRIC;
            user.Passport = objAspNetUser.Passport;
            user.PasswordHash = objAspNetUser.PasswordHash;
            user.PhoneNumber = objAspNetUser.PhoneNumber;
            user.PhoneNumberConfirmed = objAspNetUser.PhoneNumberConfirmed;
            user.Postal = objAspNetUser.Postal;
            user.State = objAspNetUser.State;
            user.UpdateDate = objAspNetUser.UpdateDate;
            user.UpdateUser = objAspNetUser.UpdateUser;
            user.User_Name = objAspNetUser.UserName;
            return user;
        }

        /// <summary>
        /// Get AspNetUser by aspNetUserId
        /// </summary>
        /// <param name="aspNetUserId"></param>
        /// <returns></returns>
        public bool IsExistUserIDInWalletUser(string pUserID)
        {
            using (var WalletUserQueryBuilder = new WalletUserQueryBuilder(new WalletEntities()))
            {
                return WalletUserQueryBuilder.HasUserId(pUserID);
            }
        }

        /// <summary>
        /// Get AspNetUser by aspNetUserId
        /// </summary>
        /// <param name="aspNetUserId"></param>
        /// <returns></returns>
        public User GetUserWalletByUserID(string pUserID)
        {
            var user = new User();
            try
            {                
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    user = new UserQueryBuilder(new WalletEntities()).HasUserId(pUserID).FirstOrDefault();
                }
                return user;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, ex, ""));
                return user;
            }

        }

        /// <summary>
        /// Get AspNetUser by aspNetUserId
        /// </summary>
        /// <param name="aspNetUserId"></param>
        /// <returns></returns>
        public Wallet_User GetWalletIDByUserID(string pUserID)
        {
            var user = new Wallet_User();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    user = new WalletUserQueryBuilder(new WalletEntities()).GetWalletIDByUserID(pUserID).FirstOrDefault();
                }
                return user;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, ex, ""));
                return user;
            }
        }

        /// <summary>
        /// Get AspNetUser by aspNetUserId
        /// </summary>
        /// <param name="aspNetUserId"></param>
        /// <returns></returns>
        public List<User_Card> GetUserCardByUserIDnCurrency(string pUserID, string pCurrency)
        {
            var user_card = new List<User_Card>();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    user_card = new UserCardQueryBuilder(new WalletEntities()).HasUserIdnCurrency(pUserID, pCurrency).ToList();
                }
                return user_card;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), pUserID + "|" + pCurrency, ex, ""));
                return user_card;
            }
        }

        /// <summary>
        /// Get AspNetUser by aspNetUserId
        /// </summary>
        /// <param name="aspNetUserId"></param>
        /// <returns></returns>
        public User_Card GetUserCardByUserAndId(string pUserID, string pId)
        {
            User_Card user_card = null;
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    user_card = new UserCardQueryBuilder(new WalletEntities()).HasUserAndId(pUserID, pId).FirstOrDefault();
                }
                return user_card;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), pUserID + "|" + pId, ex, ""));
                return user_card;
            }
        }

        /// <summary>
        /// Get AspNetUser by aspNetUserId
        /// </summary>
        /// <param name="aspNetUserId"></param>
        /// <returns></returns>
        public List<User_Card> GetUserCardByUserID(string pUserID)
        {
            var user_card = new List<User_Card>();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    user_card = new UserCardQueryBuilder(new WalletEntities()).HasUserId(pUserID).ToList();
                }
                return user_card;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, ex, ""));
                return user_card;
            }
        }


        /// <summary>
        /// Get AspNetUser by aspNetUserId
        /// </summary>
        /// <param name="aspNetUserId"></param>
        /// <returns></returns>
        public bool IsExistEmail(string pEmail)
        {
            using (var UserQueryBuilder = new UserQueryBuilder(new WalletEntities()))
            {
                return UserQueryBuilder.HasEmail(pEmail);
            }
        }

        /// <summary>
        /// Get AspNetUser by aspNetUserId
        /// </summary>
        /// <param name="aspNetUserId"></param>
        /// <returns></returns>
        public AspNetUsers GetAspNetUserByEmail(string pEmail)
        {
            var user = new AspNetUsers();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    user = new AspNetUsersQueryBuilder(new CommonEntities()).GetEmail(pEmail).FirstOrDefault();
                }
                return user;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), pEmail, ex, ""));
                return user;
            }
        }

        /// <summary>
        /// Get AspNetUser by aspNetUserId
        /// </summary>
        /// <param name="aspNetUserId"></param>
        /// <returns></returns>
        public AspNetUsers GetAspNetUserByNRIC(string pNRIC)
        {
            var user = new AspNetUsers();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    user = new AspNetUsersQueryBuilder(new CommonEntities()).GetNRIC(pNRIC).FirstOrDefault();
                }
                return user;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), pNRIC, ex, ""));
                return user;
            }
        }

        /// <summary>
        /// Get AspNetUser by aspNetUserId
        /// </summary>
        /// <param name="aspNetUserId"></param>
        /// <returns></returns>
        public AspNetUsers GetAspNetUserByPhoneNo(string pPhoneNo)
        {
            var user = new AspNetUsers();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    user = new AspNetUsersQueryBuilder(new CommonEntities()).GetPhoneNo(pPhoneNo).FirstOrDefault();
                }
                return user;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), pPhoneNo, ex, ""));
                return user;
            }
        }

        /// <summary>
        /// Get AspNetUser by aspNetUserId
        /// </summary>
        /// <param name="aspNetUserId"></param>
        /// <returns></returns>
        public List<AspNetUsers> GetAspNetUsersByIds(List<string> Ids)
        {
            var user = new List<AspNetUsers>();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    user = new AspNetUsersQueryBuilder(new CommonEntities()).HasIds(Ids).ToList();
                }
                return user;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));
                return user;
            }
        }

        public List<AspNetUsers> GetListBlockedUserID()
        {
            var user = new List<AspNetUsers>();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    user = new AspNetUsersQueryBuilder(new CommonEntities()).GetListBlockedUserID().ToList();
                }
                return user;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));
                return user;
            }
        }

    }
}
