namespace Easybook.Api.Core.Model.EasyWallet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Wallet_Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
      
        [StringLength(50)]
        public string ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Wallet_ID { get; set; }

        [Required]
        [StringLength(128)]
        public string User_ID { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; }

        [StringLength(3)]
        public string Currency_Code { get; set; }

        [Column(TypeName = "money")]
        public decimal? Available_Balance { get; set; }

        [Column(TypeName = "money")]
        public decimal? Total_Balance { get; set; }

        public DateTime CreateDate { get; set; }

        [StringLength(50)]
        public string CreateUser { get; set; }

        public DateTime UpdateDate { get; set; }

        [StringLength(50)]
        public string UpdateUser { get; set; }

        [StringLength(50)]
        public string ChecksumTotal1 { get; set; }

        [StringLength(50)]
        public string ChecksumTotal2 { get; set; }

        [StringLength(50)]
        public string ChecksumAvailable1 { get; set; }

        [StringLength(50)]
        public string ChecksumAvailable2 { get; set; }

        public bool? IsProcessing { get; set; }
        
    
    }
}
