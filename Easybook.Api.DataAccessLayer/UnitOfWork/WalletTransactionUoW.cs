using Easybook.Api.Core.Model.EasyWallet.Models;
using Easybook.Api.DataAccessLayer.QueryBuilder.Wallet;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Easybook.Api.DataAccessLayer.UnitOfWork
{
    /// <summary>
    /// Car Transaction Unit Of Work
    /// </summary>
    public class WalletTransactionUow : FluentUnitOfWork
    {
        DbContext CurrentCountryContext { set; get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork.WalletTransactionUow" /> class.
        /// </summary>
      
        public WalletTransactionUow(DbContext context) : base(context)
        {
            CurrentCountryContext = context;
        }

        /// <param name="EWallet_Info">The cart.</param>
        /// <param name="source"></param>
        /// <returns></returns> 
        private bool InsertWalletAccount(Wallet_Account WalletAccount, string source)
        {
            var Wallet_Acc = new Wallet_Account
            {
                Currency_Code = WalletAccount.Currency_Code,
                Available_Balance = WalletAccount.Available_Balance,
                Total_Balance = WalletAccount.Total_Balance,
                ChecksumAvailable1 = WalletAccount.ChecksumAvailable1,
                ChecksumAvailable2 = WalletAccount.ChecksumAvailable2,
                ChecksumTotal1 = WalletAccount.ChecksumTotal1,
                ChecksumTotal2 = WalletAccount.ChecksumTotal2,                
                CreateDate = WalletAccount.CreateDate,
                CreateUser = WalletAccount.CreateUser,                
                Wallet_ID = WalletAccount.Wallet_ID,
                ID = WalletAccount.ID,
                Remarks = WalletAccount.Remarks,
                UpdateDate = WalletAccount.UpdateDate,
                UpdateUser = WalletAccount.UpdateUser               
            };        

            return
                    BeginTransaction()
                        // First Part
                        .DoInsert(Wallet_Acc)                       
                        .SaveAndContinue()
                        // Second Part                      
                    .EndTransaction();
        }

        private bool InsertUser(User User, string source)
        {
            var Wallet_User = new User
            {
                Address = User.Address,
                City = User.City,
                DOB = User.DOB,
                Email = User.Email,
                EmailConfirmed = User.EmailConfirmed,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Nationality = User.Nationality,
                NRIC = User.NRIC,
                Passport = User.Passport,
                PasswordHash = User.PasswordHash,
                PhoneNumber = User.PhoneNumber,
                PhoneNumberConfirmed = User.PhoneNumberConfirmed,
                Postal = User.Postal,
                State = User.State,
                User_Name = User.User_Name               
            };

            return
                    BeginTransaction()
                        // First Part
                        .DoInsert(Wallet_User)
                        .SaveAndContinue()
                    // Second Part                      
                    .EndTransaction();
        }

        private bool InsertWalletSubTransaction(SubTransaction Wallet_SubTransaction, string source)
        {
            var WalletSubTransaction = new SubTransaction
            {
                Amount = Wallet_SubTransaction.Amount,                
                CreateDate = Wallet_SubTransaction.CreateDate,
                CreateUser = Wallet_SubTransaction.CreateUser,
                Currency_Code = Wallet_SubTransaction.Currency_Code,
                Account_ID = Wallet_SubTransaction.Account_ID,
                Direction = Wallet_SubTransaction.Direction,               
                Verified = Wallet_SubTransaction.Verified,
                Checksum1 = Wallet_SubTransaction.Checksum1,
                Checksum2 = Wallet_SubTransaction.Checksum2,                
                Remarks = Wallet_SubTransaction.Remarks,
                Sub_ID = Wallet_SubTransaction.Sub_ID,
                Tran_ID = Wallet_SubTransaction.Tran_ID
            };

            return
                    BeginTransaction()
                        // First Part
                        .DoInsert(WalletSubTransaction)
                        .SaveAndContinue()
                    // Second Part                      
                    .EndTransaction();
        }

        private bool InsertWalletTransaction(Transaction Wallet_Transaction, string source)
        {
            var WalletTransaction = new Transaction
            {
                Destination_Amount = Wallet_Transaction.Destination_Amount,
                Destination_Currency = Wallet_Transaction.Destination_Currency,
                CreateDate = Wallet_Transaction.CreateDate,
                CreateUser = Wallet_Transaction.CreateUser,
                Source_Currency = Wallet_Transaction.Source_Currency,
                Source_Amount = Wallet_Transaction.Source_Amount,
                Description = Wallet_Transaction.Description,
                Merchant_ref = Wallet_Transaction.Merchant_ref,
                Wallet_ID = Wallet_Transaction.Wallet_ID,
                Remarks = Wallet_Transaction.Remarks,
                Status = Wallet_Transaction.Status,
                Source = Wallet_Transaction.Source,
                Tran_ID = Wallet_Transaction.Tran_ID,
                PaymentGateway = Wallet_Transaction.PaymentGateway
            };

            return
                    BeginTransaction()
                        // First Part
                        .DoInsert(WalletTransaction)
                        .SaveAndContinue()
                    // Second Part                      
                    .EndTransaction();
        }

        private bool InsertWalletUser(Wallet_User WalletUser, string source)
        {
            var Wallet_User = new Wallet_User
            {
                BlockChainID=WalletUser.BlockChainID,
                User_ID = WalletUser.User_ID,
                Wallet_ID = WalletUser.Wallet_ID               
            };

            return
                    BeginTransaction()
                        // First Part
                        .DoInsert(Wallet_User)
                        .SaveAndContinue()
                    // Second Part                      
                    .EndTransaction();
        }
        /// <summary>
        /// Determines whether the specified cart has unique identifier.
        /// </summary>
        /// <param name="WalletID">The cart unique identifier.</param>
        /// <returns></returns>
        public Wallet_Account GetWalletByID(string AccountID)
        {
            var WalletAccountQueryBuilder = new WalletAccountQueryBuilder(CurrentCountryContext);            
            return WalletAccountQueryBuilder.GetWalletByID(AccountID).FirstOrDefault();
         
        }

        public List<Wallet_Account> GetWalletByIDs(List<string> accs)
        {
            return new WalletAccountQueryBuilder(CurrentCountryContext).GetWalletByIDs(accs).ToList();
        }

        public Wallet_Account_Reward GetRewardByAccID(string AccountID)
        {
            var TransactionRewardsQueryBuilder = new WalletRewardQueryBuilder(CurrentCountryContext);
            return TransactionRewardsQueryBuilder.GetRewardByAccID(AccountID).FirstOrDefault();

        }
        public List<Wallet_Account_Reward> GetAllRewardAccount()
        {
            var TransactionRewardsQueryBuilder = new WalletRewardQueryBuilder(CurrentCountryContext);
            return TransactionRewardsQueryBuilder.GetAllRewardAcc().ToList();

        }

        public List<Wallet_Account> GetAllWalletAccountByDate(DateTime pDate)
        {
            var WalletAccountQueryBuilder = new WalletAccountQueryBuilder(CurrentCountryContext);
            return WalletAccountQueryBuilder.GetAllWalletAccountByDate(pDate).ToList();
        }

        public List<Wallet_Account> GetAllWalletAccountByDateWithoutCS(DateTime pDate)
        {
            var WalletAccountQueryBuilder = new WalletAccountQueryBuilder(CurrentCountryContext);
            return WalletAccountQueryBuilder.GetAllWalletAccountByDateWithoutCS(pDate).ToList();
        }
        

        public List<Wallet_Account> GetAllWalletAccount()
        {
            var WalletAccountQueryBuilder = new WalletAccountQueryBuilder(CurrentCountryContext);
            return WalletAccountQueryBuilder.GetAllWalletAccount().ToList();
        }

        public List<SubTransaction> GetAllSubTran(DateTime pDateTime)
        {
            var SubTransactionQueryBuilder = new SubTransactionQueryBuilder(CurrentCountryContext);
            return SubTransactionQueryBuilder.GetAllSubTran(pDateTime).ToList();
        }

        public List<Wallet_Snapshot> GetSnapshotByCheckSum(string pCheckSum)
        {
            var WalletSnapshotQueryBuilder = new WalletSnapshotQueryBuilder(CurrentCountryContext);
            return WalletSnapshotQueryBuilder.GetSnapshotByCheckSum(pCheckSum).ToList();
        }

        public List<Wallet_Account> GetWalletAccountByUserID(string pUserID)
        {
            var WalletAccountQueryBuilder = new WalletAccountQueryBuilder(CurrentCountryContext);
            return WalletAccountQueryBuilder.GetWalletByUserID(pUserID).ToList();

        }
        public Transaction GetTranByTranID(string pTranID)
        {
            var TransactionQueryBuilder = new TransactionQueryBuilder(CurrentCountryContext);
            return TransactionQueryBuilder.GetTranByTranID(pTranID).FirstOrDefault();

        }
        public string GetLastCheckSum1()
        {
            var SubTransactionQueryBuilder = new SubTransactionQueryBuilder(CurrentCountryContext);
            return SubTransactionQueryBuilder.GetLastCheckSum1();

        }
        public User_Bank_Account GetBankAccByID(string ID)
        {          
            return new UserBankAccountQueryBuilder(CurrentCountryContext).HasID(ID).FirstOrDefault();
        }

        ///// <summary>
        ///// Performs the rent.
        ///// </summary>
        ///// <param name="memberId">The member identifier.</param>
        ///// <param name="carId">The car identifier.</param>
        ///// <param name="pickUpSubPlaceId">The pick up sub place identifier.</param>
        ///// <param name="dropOffSubPlaceId">The drop off sub place identifier.</param>
        ///// <param name="pickUpAddress">The pick up address.</param>
        ///// <param name="dropOffAddress">The drop off address.</param>
        ///// <param name="rentPurpose">The rent purpose.</param>
        ///// <param name="remark">The remark.</param>
        ///// <param name="rentFrom">The rent from.</param>
        ///// <param name="rentTo">The rent to.</param>
        ///// <param name="nationality">The nationality.</param>
        ///// <param name="amount">The amount.</param>
        ///// <param name="remainingAmount">The remaining amount.</param>
        ///// <param name="source">The source.</param>
        ///// <returns></returns>
        //public string PerformRent(int memberId, int carId, int pickUpSubPlaceId, int dropOffSubPlaceId, string pickUpAddress, string dropOffAddress, string rentPurpose, string remark, DateTime rentFrom, DateTime rentTo, Nationality nationality, decimal amount, decimal remainingAmount, string source)
        //{
        //    var car = _carQueryBuilder.HasId(carId).FirstOrDefault();
        //    var pickupSubPlace = _subPlaceQueryBuilder.HasId(pickUpSubPlaceId).FirstOrDefault();
        //    var dropOffSubPlace = _subPlaceQueryBuilder.HasId(dropOffSubPlaceId).FirstOrDefault();

        //    if (car == null) { return "Car not found"; }
        //    if (pickupSubPlace == null) { return "Pick up place not found"; }
        //    if (dropOffSubPlace == null) { return "Drop off place not found"; }

        //    var cart = new Cart
        //    {
        //        CartGuid = Guid.NewGuid().ToString(),
        //        Amount = amount,
        //        CarId = carId,
        //        CreateDate = DateTime.Now,
        //        CreateUser = "API",
        //        MemberId = memberId,
        //        PickUpSubPlaceId = pickUpSubPlaceId,
        //        DropOffSubPlaceId = dropOffSubPlaceId,
        //        PickUpLocation = pickUpAddress,
        //        DropOffLocation = dropOffAddress,
        //        RentFrom = rentFrom,
        //        RentTo = rentTo,
        //        RentPurpose = rentPurpose,
        //        Status = "success",
        //        RemainingAmount = remainingAmount,
        //        OriginalAmount = amount + remainingAmount,
        //        Currency = car.Currency,
        //        OriginalCurrency = car.Currency,
        //        PaymentGateway = "API",
        //        Remark = remark
        //    };

        //    return !InsertTransaction(cart, source) ? "Unexpected error" : string.Empty;
        //}

    }
}
