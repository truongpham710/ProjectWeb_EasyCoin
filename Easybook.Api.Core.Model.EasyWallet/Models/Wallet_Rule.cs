namespace Easybook.Api.Core.Model.EasyWallet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Wallet_Rule
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Account_ID { get; set; }

        [Column(TypeName = "money")]
        public decimal? Maximum_Topup_Amount { get; set; }

        [Column(TypeName = "money")]
        public decimal? Maximum_Withdraw_Amount { get; set; }

        public decimal? Minimum_Topup_Amount { get; set; }        

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string Currency_Code { get; set; }

        public virtual Wallet_Account Wallet_Account { get; set; }
    }
}
