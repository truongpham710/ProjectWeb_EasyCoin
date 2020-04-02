namespace Easybook.Api.Core.Model.EasyWallet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Wallet_Snapshot
    {
        [StringLength(50)]
        public string ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Account_ID { get; set; }
        
        public decimal? Balance { get; set; }

        [StringLength(3)]
        public string Currency_Code { get; set; }

        public DateTime CreateDate { get; set; }

        [StringLength(50)]
        public string Checksum { get; set; }

        [StringLength(50)]
        public string Snapshot { get; set; }

        public DateTime UpdateDate { get; set; }

        public DateTime CreateDateSnapshot { get; set; }

        public decimal? Reward_Amount { get; set; }
    }
}
