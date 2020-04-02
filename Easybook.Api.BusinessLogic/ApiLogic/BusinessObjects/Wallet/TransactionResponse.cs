using Easybook.Api.Core.Model.EasyWallet.DataTransferObject;
using Easybook.Api.Core.Model.EasyWallet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Easybook.Api.BusinessLogic.ApiLogic.BusinessObjects.Wallet
{
    public class TransactionResponse : Response
    {
        [Required]
        public string Wallet_ID { get; set; }
        public string Description { get; set; }
        public string Source_Currency { get; set; }     
        public decimal? Source_Amount { get; set; }      
        public string Destination_Currency { get; set; }     
        public decimal? Destination_Amount { get; set; }     
        public string TranStatus { get; set; }
        public DateTime? CreateDate { get; set; }   
        public string CreateUser { get; set; }   
        public string Remarks { get; set; }    
        public string Source { get; set; }      
        public string Merchant_ref { get; set; }
        public string User_ID { get; set; }        
        public string PaymentGateWay { get; set; }

        [Required]
        public string TranID { get; set; }
    }

    public class ListTransactionResponse : Response
    {
        public List<TransactionTransferObject> lstTrans { get; set; }

    }

    public class HistorySubTransaction
    {
        public string Sub_ID { get; set; }
        public string Tran_ID { get; set; }
        public string Account_ID { get; set; }
        public string Source_Currency { get; set; }
        public decimal? Source_Amount { get; set; }
        public string TransactionType { get; set; }
        public bool? Verified { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Remarks { get; set; }
        public string PaymentGateway { get; set; }
        public string Merchant_ref { get; set; }
        public string Source { get; set; }
        public string CreateUser { get; set; }
        public decimal Balance { get; set; }
        public string BusCompanyName { get; set; }
        public string FromStationName { get; set; }
        public string ToStationName { get; set; }
        public decimal TotalDistance { get; set; }
        public bool IsAutoCheckOut { get; set; }
        public string ChargeType { get; set; }

    }

    public class ListSubTransactionResponse : Response
    {
        public List<HistorySubTransaction> lstSubTrans { get; set; }
        public int RecordTotal { get; set; }


        public ListSubTransactionResponse()
        {
            RecordTotal = 0;
            lstSubTrans = new List<HistorySubTransaction>();
        }

    }

    public class UpdateTranBQResponse : Response
    {
        public bool StatusUpdate { get; set; }

    }

    public class SubTransactionReponse : Response
    {
        public string subTranID { get; set; }

    }

    public class SnapShotResponse : Response
    {
        /// <summary>
        /// Response object for location
        /// </summary>
        public string SnapshotID { get; set; }

    }

    public class InterestResponse : Response
    {
        /// <summary>
        /// Response object for location
        /// </summary>
        public string InterestID { get; set; }
        public List<Transaction> lstTrans { get; set; }

    }

    public class CancelWithdrawResponse : Response { }

    public class PendWithdrawResponse : Response { }

    public class RefundTransactionResponse : Response { }

    public class ViralTransactionResponse : Response {
        public bool FirstTimeTopUp { get; set; }
        public string CurrencyCode { get; set; }
        public decimal TopupAmount { get; set; } = 0;
        public decimal TotalTopUpAmount { get; set; } = 0;
        public string UserID { get; set; }
       
    }
    public class UserBankAccountResponse : Response
    {
        /// <summary>
        /// Response object for location
        /// </summary>
        public User_Bank_Account userBank { get; set; }

    }

    public class lstUserBankAccountResponse : Response
    {
        /// <summary>
        /// Response object for location
        /// </summary>
        public List<User_Bank_Account> lstUserBankAcc { get; set; }

    }
    public class PromotionContentResponse : Response
    {
        public string HTMLPagePromotionContent { get; set; }

    }
    public class CalculationWithdrawalFeesResponse : Response {
        public decimal ReceiptAmount { get; set; }
        public decimal BalanceAmountLeft { get; set; }
        public decimal FeeA { get; set; }
        public decimal FeeB { get; set; }
        public decimal FeeC { get; set; }
        public string FeeC_Percent { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal WithdrawAmount { get; set; }
        public decimal TotalFee { get; set; }


    }
}
