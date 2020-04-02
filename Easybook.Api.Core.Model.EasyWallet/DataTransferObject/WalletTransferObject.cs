using System;

namespace Easybook.Api.Core.Model.EasyWallet.DataTransferObject
{
    public class WalletTransferObject
    {       
        public decimal? Available_Balance { get; set; }      
        public string ChecksumAvailable1 { get; set; }      
        public string ChecksumAvailable2 { get; set; }      
        public string ChecksumTotal1 { get; set; }       
        public string ChecksumTotal2 { get; set; }
        public DateTime? CreateDate { get; set; }       
        public string CreateUser { get; set; }      
        public string Currency_Code { get; set; }     
        public string ID { get; set; }
        public bool? IsProcessing { get; set; }        
        public string Remarks { get; set; }      
        public decimal? Total_Balance { get; set; }
        public DateTime? UpdateDate { get; set; }    
        public string UpdateUser { get; set; } 
        public string User_ID { get; set; }     
        public string Wallet_ID { get; set; }
        public decimal? InterestRate { get; set; }
        public decimal InterestEarned { get; set; }
        public decimal RewardAmount { get; set; }
    }

    public class TransactionTransferObject
    {
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
        public string TranID { get; set; }
        public string Scheduler_Check { get; set; }
        public string Data_Check { get; set; }
        public string Status_Verify { get; set; }
        public string Money_In { get; set; }
        public string Money_Out { get; set; }
        public decimal? Balance { get; set; }
    }

    public class UserCardTransferObject
    {
        public string ID { get; set; }
        public string Card_HolderName { get; set; }
        public string Card_Number { get; set; }
        public string Card_Type { get; set; }
        public string Card_ExpireDate { get; set; }
        public string Currency_Code { get; set; }
        public string Bank_Name { get; set; }
        public DateTime Create_date { get; set; }
        public DateTime Update_date { get; set; }
        public string User_ID { get; set; }
        public string Sign { get; set; }

    }
}