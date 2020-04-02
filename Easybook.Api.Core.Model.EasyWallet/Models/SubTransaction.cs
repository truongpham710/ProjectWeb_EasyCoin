namespace Easybook.Api.Core.Model.EasyWallet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SubTransaction")]
    public partial class SubTransaction
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Sequent_ID { get; set; }
        [Key]
        [StringLength(50)]
        public string Sub_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Tran_ID { get; set; }

        [StringLength(50)]
        public string Account_ID { get; set; }

        [StringLength(128)]
        public string User_ID { get; set; }

        [StringLength(3)]
        public string Currency_Code { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        [StringLength(10)]
        public string Direction { get; set; }

        public bool? Verified { get; set; }

        public DateTime CreateDate { get; set; }        

        [StringLength(50)]
        public string CreateUser { get; set; }
      
        [StringLength(500)]
        public string Remarks { get; set; }

        [StringLength(50)]
        public string Checksum1 { get; set; }

        [StringLength(50)]
        public string Checksum2 { get; set; }
    }
}
