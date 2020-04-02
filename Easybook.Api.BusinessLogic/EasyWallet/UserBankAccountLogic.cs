using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Easybook.Api.DataAccessLayer.UnitOfWork;
using Easybook.Api.Core.Model.EasyWallet.Models;
using Easybook.Api.BusinessLogic.EasyWallet.Utility;
using System.Reflection;
using Easybook.Api.DataAccessLayer.QueryBuilder.Wallet;
using Easybook.Api.BusinessLogic.ApiLogic.BusinessObjects.Wallet;
using Easybook.Api.Core.CrossCutting.Utility;
using Easybook.Api.BusinessLogic.EasyWallet.Constant;
using System.IO;
using System.Drawing;
namespace Easybook.Api.BusinessLogic.EasyWallet
{
    public class UserBankAccountLogic
    {
        

        public List<User_Bank_Account> LoadVerificationStatus(string userID, string currency)
        {
            List<string> messages = new List<string>();
            var userBank = new List<User_Bank_Account>();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    userBank = new UserBankAccountQueryBuilder(new WalletEntities()).GetInfoUserBankAccount(userID, currency).ToList();
                }
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));
               
               
            }
            return userBank;
        }

        /// <summary>
        /// Get AspNetUser by aspNetUserId
        /// </summary>
        /// <param name="aspNetUserId"></param>
        /// <returns></returns>
        public List<User_Bank_Account> GetUserBankAccByUserID(string pUserID)
        {
            var user_bank = new List<User_Bank_Account>();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    user_bank = new UserBankAccountQueryBuilder(new WalletEntities()).GetInfoUserBankAccountByUserID(pUserID).ToList();
                }
                return user_bank;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, ex, ""));
                return user_bank;
            }
        }

        /// <summary>
        /// Get AspNetUser by aspNetUserId
        /// </summary>
        /// <param name="aspNetUserId"></param>
        /// <returns></returns>
        public List<User_Bank_Account> GetBankAccwithoutPending()
        {
            var user_bank = new List<User_Bank_Account>();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    user_bank = new UserBankAccountQueryBuilder(new WalletEntities()).GetBankAccwithoutPending().ToList();
                }
                return user_bank;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));
                return user_bank;
            }
        }

        public bool DeleteUserBankAcc(string IDBankAcc)
        {
            try
            {
                using (var eWalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities()))
                {
                    eWalletTransactionUnitOfWork.BeginTransaction();
                    var UserBankAcc = new UserBankAccountQueryBuilder(new WalletEntities()).HasID(IDBankAcc).FirstOrDefault();
                    eWalletTransactionUnitOfWork.DoDelete(UserBankAcc);
                    eWalletTransactionUnitOfWork.EndTransaction();
                }
                return true;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), IDBankAcc, ex, ""));
                return false;
            }
        }

        public bool UpdateVerificationStatus(UserBankAccountRequest request)
        {
            if (SimpleAesUtil.DecryptAES(request.AccountNumber, EwalletConstant.keyAES).IndexOf(EwalletConstant.strWord) == -1)
            {
                return false;
            }
            WalletTransactionUow WalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities());
            try
            {
                WalletTransactionUnitOfWork.BeginTransaction();
                var UserBankAcc = WalletTransactionUnitOfWork.GetBankAccByID(request.ID);
                UserBankAcc.Comments = request.Comments;
                UserBankAcc.Verify = request.Verify;
                UserBankAcc.Update_date = DateTime.Now;                   
                WalletTransactionUnitOfWork.DoUpdate(UserBankAcc).SaveAndContinue();
                WalletTransactionUnitOfWork.EndTransaction();
                return true;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));
                return false;
            }
        }
        public bool InsertVerificationStatus(UserBankAccountRequest request)
        {
            try
            {
            var BankAcc = SimpleAesUtil.DecryptAES(request.AccountNumber, EwalletConstant.keyAES);
            BankAcc = BankAcc.Replace(EwalletConstant.strWord, "").Replace(" ", "").Replace("-", "");
            double Num;
            bool isNum = double.TryParse(BankAcc, out Num);

            if (!isNum || BankAcc.Length < 8 || BankAcc.Length > 20)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "BankAcc: " + BankAcc, null, "Issue for BankACC");
                return false;
            }
            
            byte[] bytes = Convert.FromBase64String(request.urlBankAcc);
            request.FileNameBankAcc = "BankAcc_" + DateTime.Now.Ticks + request.FileNameBankAcc.Substring(request.FileNameBankAcc.LastIndexOf('.'), 4);
            var pathFileNameBankAcc = Path.Combine(EwalletConstant.EWalletPathPictureUpload, request.FileNameBankAcc);
            using (Image image = Image.FromStream(new MemoryStream(bytes)))
            {
                image.Save(pathFileNameBankAcc); 
            }

            bytes = Convert.FromBase64String(request.urlPassIC);
            request.FileNamePasIC = "BankPasIC_" + DateTime.Now.Ticks + request.FileNamePasIC.Substring(request.FileNamePasIC.LastIndexOf('.'), 4);
            var pathFileNamePasIC = Path.Combine(EwalletConstant.EWalletPathPictureUpload, request.FileNamePasIC);
            using (Image image = Image.FromStream(new MemoryStream(bytes)))
            {
                image.Save(pathFileNamePasIC);
            }

            var userBankAccount = new User_Bank_Account
            {
                ID = SecurityLogic.GenerateKey(30),
                BankCurrency = request.BankCurrency,
                CountryBank = request.CountryBank,
                BankName = request.BankName,
                AccountName = request.AccountName,
                AccountNumber = request.AccountNumber,
                Verify = "Pending",
                urlBankAcc = pathFileNameBankAcc,
                urlPassIC = pathFileNamePasIC,
                User_ID = request.User_ID,
                BankCity = request.BankCity,
                BranchCode = request.BranchCode,
                BranchName = request.BranchName,
                Comments = request.Comments,
                Create_date = DateTime.Now,
                Update_date = DateTime.Now,

            };
           
            
            WalletTransactionUow WalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities());
            
                WalletTransactionUnitOfWork.BeginTransaction();
                WalletTransactionUnitOfWork.DoInsert(userBankAccount).SaveAndContinue();
                WalletTransactionUnitOfWork.EndTransaction();
                return true;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), request, ex, ""));
                return false;
            }
        }
        public List<User_Bank_Account> LoadAllUserBankAccount()
        {
            List<string> messages = new List<string>();
            var userBankLst = new List<User_Bank_Account>();
            try
            {
                userBankLst = new UserBankAccountQueryBuilder(new WalletEntities()).GetAllUserBankAccount().ToList();

            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));


            }
            return userBankLst;
        }
        public User_Bank_Account UserBankAccountByID(string id)
        {
            List<string> messages = new List<string>();
            var userBank = new User_Bank_Account();
            try
            {
                userBank = new UserBankAccountQueryBuilder(new WalletEntities()).UserBankAccount_ByID(id).FirstOrDefault();

            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));
            }
            return userBank;
        }
    }
}
