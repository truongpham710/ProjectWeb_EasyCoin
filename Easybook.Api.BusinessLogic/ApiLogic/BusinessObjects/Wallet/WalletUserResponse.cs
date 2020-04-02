using Easybook.Api.Core.Model.EasyWallet.DataTransferObject;
using Easybook.Api.Core.Model.EasyWallet.Models;
using System;
using System.Collections.Generic;

namespace Easybook.Api.BusinessLogic.ApiLogic.BusinessObjects.Wallet
{
    public class WalletUserResponse : Response
    {
        /// <summary>
        /// Response object for location
        /// </summary>
        public string WalletID { get; set; }

    }

    public class UserResponse : Response
    {
        /// <summary>
        /// Response object for location
        /// </summary>
        public string UserID { get; set; }

    }

    public class UserCardResponse : Response
    {
        /// <summary>
        /// Response object for location
        /// </summary>
        public string CardID { get; set; }

    }

    public class CreditCardResponse : Response
    {
        public User_Card card { get; set; }
    }

    public class ListUserCardResponse : Response
    {
        public List<User_Card> lstUserCard { get; set; }

    }

    public class TranResponse : Response
    {
        public string TranID { get; set; }
        public string SubID { get; set; }
       
    }
    public class TranTownBusResponse : Response
    {
        public string TranID { get; set; }
        // public string SubID { get; set; }
        public decimal AvailableBalance { get; set; }
        public decimal FeeMaxCharge { get; set; }
        public decimal FeeRefunded { get; set; }
        public string CoordinationName { get; set; }
        //public string CoordinationNameOut { get; set; }
        public DateTime CreateDate { get; set; }
        // public long TimestampOut { get; set; }
        public decimal TotalDistance { get; set; }
        public decimal TotalFeeCharged { get; set; }
        public decimal BusTripFree { get; set; }
        public string Currency { get; set; }
        public string ChargeType { get; set; }
        public decimal Reward { get; set; }
        public decimal MainCash  { get; set; }

       // public bool IsGetFreeRides { get; set; } = false;

    }
    public class ListPendingTranTownBusResponse : Response
    {
        public ListPendingTranTownBusResponse()
        {
            lstPeddingTrans = new List<PendingTranTownBusResponse>();
        }
       public List<PendingTranTownBusResponse> lstPeddingTrans { get; set; }
    }
    public class PendingTranTownBusResponse
    {
        public int TripId { get; set; }
        public string TranID { get; set; }
        public decimal AvailableBalance { get; set; }
        public decimal Reward { get; set; }
        public decimal FeeMaxCharge { get; set; }
        public string CoordinationName { get; set; }
        public DateTime CreateDate { get; set; }
        public string Currency { get; set; }
        public string ChargeType { get; set; }
        public long Timestamp { get; set; }
        public decimal TotalFeeCharged { get; set; }
        public string Credential { get; set; }



    }
    public class WalletAccountResponse : Response
    {      
        public List<WalletTransferObject> lstWalletAccount { get; set; }
        public string Remarks { get; set; }

    }

    public class WalletAmountResponse : Response
    {
        public decimal AvailableAmount { get; set; }
        public decimal RewardAmount { get; set; }

    }

    public class WalletUserAccountResponse : Response
    {
      
        public string AccountID { get; set; }

    }
}
