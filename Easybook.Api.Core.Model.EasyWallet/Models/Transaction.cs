namespace Easybook.Api.Core.Model.EasyWallet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transaction")]
    public partial class Transaction
    {

        [Key]
        [StringLength(50)]
        public string Tran_ID { get; set; }

        [StringLength(50)]
        public string Wallet_ID { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(3)]
        public string Source_Currency { get; set; }

        [Column(TypeName = "money")]
        public decimal? Source_Amount { get; set; }

        [StringLength(3)]
        public string Destination_Currency { get; set; }

        [Column(TypeName = "money")]
        public decimal? Destination_Amount { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateUser { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; }

        [StringLength(100)]
        public string PaymentGateway { get; set; }

        [StringLength(200)]
        public string Source { get; set; }

        [StringLength(200)]
        public string Merchant_ref { get; set; }

        [StringLength(128)]
        public string User_ID { get; set; }

        [StringLength(20)]
        public string Scheduler_Check { get; set; }

        public DateTime? UpdateDate { get; set; }

        [StringLength(200)]
        public string UpdateUser { get; set; }


    }
}
