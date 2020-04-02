using System;
using System.ComponentModel.DataAnnotations;

namespace Easybook.Api.BusinessLogic.ApiLogic.BusinessObjects.Wallet
{
    public class TransactionRequest 
    {
        [Required]
        public string Wallet_ID { get; set; }
        public string Description { get; set; }
        public string Source_Currency { get; set; }     
        public decimal? Source_Amount { get; set; }      
        public string Destination_Currency { get; set; }     
        public decimal? Destination_Amount { get; set; }     
        public string Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateUser { get; set; }
        public string Remarks { get; set; }    
        public string Source { get; set; }      
        public string Merchant_ref { get; set; }
        public string User_ID { get; set; }        
        public string PaymentGateWay { get; set; }
        public string Tran_ID { get; set; }
        [Required]
        public string AccID { get; set; }
        public string sign { get; set; }
    }
}
