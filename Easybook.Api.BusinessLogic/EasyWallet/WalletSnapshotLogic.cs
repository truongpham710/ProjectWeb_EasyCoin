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
using static Easybook.Api.BusinessLogic.EasyWallet.Utility.SecurityLogic;
using System.Text;

namespace Easybook.Api.BusinessLogic.EasyWallet
{
    public class WalletSnapshotLogic
    {

        public string BuildCheckSum(Wallet_Snapshot pSnapshot)
        {
            string CheckSum = SecurityLogic.GetSha1Hash(pSnapshot.ID + "|" + pSnapshot.Account_ID + "|" + ConvertUtility.RoundToTwoDecimalPlaces(pSnapshot.Balance) + "|" + ConvertUtility.RoundToTwoDecimalPlaces(pSnapshot.Reward_Amount??0) + "|" + pSnapshot.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "|" + pSnapshot.Currency_Code + "|" + Globals.StampServerKey);
            return CheckSum;
        }

        public bool InsertSnapshot(string now)
        {
            var lstRewardAcc = new List<Wallet_Account_Reward>();
            var lstWalletSnapshot = new List<Wallet_Snapshot>();
            var userLogic = new WalletUserLogic(true);
            try
            {
                var DateSnapshot = new WalletSnapshotQueryBuilder(new WalletEntities()).GetLatestDateSnapshot().FirstOrDefault().CreateDateSnapshot;
                if (DateSnapshot.Year == int.Parse(now.Split('-')[0]) && DateSnapshot.Month == int.Parse(now.Split('-')[1]) && DateSnapshot.Day == int.Parse(now.Split('-')[2]))
                    return true;
                WalletTransactionUow WalletTransactionUnitOfWork = null;
                using (WalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities()))
                {
                    WalletTransactionUnitOfWork.BeginTransaction();
                    var lstWalletAccount = WalletTransactionUnitOfWork.GetAllWalletAccount().OrderBy(wa=>wa.ID);
                    lstRewardAcc = WalletTransactionUnitOfWork.GetAllRewardAccount();
                    string S1 = "";
                    foreach (Wallet_Account walletAcc in lstWalletAccount)
                    {
                        var walletSnapShot = new Wallet_Snapshot();
                        walletSnapShot.ID = Guid.NewGuid().ToString();
                        walletSnapShot.Account_ID = walletAcc.ID;
                        walletSnapShot.Balance = walletAcc.Available_Balance;
                        if (lstRewardAcc != null)
                        {
                            var rewardACC = lstRewardAcc.Find(p => p.ID == walletAcc.ID);
                            if (rewardACC != null)
                                walletSnapShot.Reward_Amount = rewardACC.Reward_Amount;
                            else
                                walletSnapShot.Reward_Amount = 0;
                        }
                        else
                        walletSnapShot.Reward_Amount = 0;
                        walletSnapShot.CreateDate = walletAcc.CreateDate;
                        walletSnapShot.UpdateDate = walletAcc.UpdateDate;
                        walletSnapShot.CreateDateSnapshot = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
                        walletSnapShot.Currency_Code = walletAcc.Currency_Code;
                        walletSnapShot.Checksum = BuildCheckSum(walletSnapShot);
                        lstWalletSnapshot.Add(walletSnapShot);
                        S1 += walletSnapShot.ID + walletSnapShot.Account_ID + ConvertUtility.RoundToTwoDecimalPlaces(walletSnapShot.Balance) + ConvertUtility.RoundToTwoDecimalPlaces(walletSnapShot.Reward_Amount) + walletSnapShot.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + walletSnapShot.Currency_Code + walletSnapShot.Checksum;
                    }
                    var S1Hash = SecurityLogic.GetSha1Hash(S1);
                    foreach (Wallet_Snapshot Wallet_Snapshot in lstWalletSnapshot)
                    {
                        Wallet_Snapshot.Snapshot = S1Hash;
                    }

                    WalletTransactionUnitOfWork.DoInsertMany(lstWalletSnapshot).EndTransaction();

                }
                return true;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));
                return false;
            }
        }

        public bool VerifySnapshot(string snapshotChecksum, out List<Wallet_Snapshot> snapshot)
        {
            bool result = false;
            snapshot = null;
            try
            {
                //var maxDate = new WalletSnapshotQueryBuilder(new WalletEntities()).AsQueryable().Max(s => s.CreateDateSnapshot);
                snapshot = new WalletSnapshotQueryBuilder(new WalletEntities()).HasSnapshotChecksum(snapshotChecksum).ToList();
                string S1 = string.Join("", snapshot.OrderBy(s => s.Account_ID).Select(r => r.ID + r.Account_ID + ConvertUtility.RoundToTwoDecimalPlaces(r.Balance) + ConvertUtility.RoundToTwoDecimalPlaces(r.Reward_Amount) + r.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + r.Currency_Code + r.Checksum));
                var S1Hash = SecurityLogic.GetSha1Hash(S1);
                result = S1Hash == snapshotChecksum;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));
                result = false;
            }
            return result;
        }

        public bool UpdateCheckSumSnapshot(string pChecksum)
        {
            var lstSnapshots = new List<Wallet_Snapshot>();
            var userLogic = new WalletUserLogic(true);
            try
            {
                var logWallet = new LogWallet();
                WalletTransactionUow WalletTransactionUnitOfWork = null;
                using (WalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities()))
                {
                    
                    lstSnapshots = WalletTransactionUnitOfWork.GetSnapshotByCheckSum(pChecksum);
                    logWallet.Log(MethodBase.GetCurrentMethod(), "Log here 1: " + pChecksum  + "|"+ lstSnapshots.Count, null, "");
                    var S1 = "";
                    foreach (Wallet_Snapshot walletSnapShot in lstSnapshots)
                    {
                        walletSnapShot.Checksum = BuildCheckSum(walletSnapShot);
                        S1+=(walletSnapShot.ID + walletSnapShot.Account_ID + ConvertUtility.RoundToTwoDecimalPlaces(walletSnapShot.Balance) + walletSnapShot.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + walletSnapShot.Currency_Code + walletSnapShot.Checksum);
                    }
                    logWallet.Log(MethodBase.GetCurrentMethod(), "Log here 2", null, "");
                    var S1Hash = SecurityLogic.GetSha1Hash(S1);
                    logWallet.Log(MethodBase.GetCurrentMethod(), "Log here 3" + S1Hash, null, "");
                    foreach (Wallet_Snapshot Wallet_Snapshot in lstSnapshots)
                    {
                        Wallet_Snapshot.Snapshot = S1Hash;
                    }

                    WalletTransactionUnitOfWork.DoUpdateMany(lstSnapshots).EndTransaction();

                    logWallet.Log(MethodBase.GetCurrentMethod(), "Finish Update UpdateCheckSumSnapshot", null, "");
                }

                return true;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));
                return false;
            }
        }
    }
}
