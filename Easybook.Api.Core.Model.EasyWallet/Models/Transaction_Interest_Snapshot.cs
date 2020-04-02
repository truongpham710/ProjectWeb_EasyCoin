namespace Easybook.Api.Core.Model.EasyWallet.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Transaction_Interest_Snapshot")]
    public partial class Transaction_Interest_Snapshot
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        [Key]
        public string ID { get; set; }
        public string Account_ID { get; set; }
        public string Tran_ID { get; set; }
        public decimal Interest_Amount { get; set; }
        public decimal Total_Amount { get; set; }
        public DateTime Createdate { get; set; }
        public string Remark { get; set; }
        public string CheckSumInterest { get; set; }
    }
}
