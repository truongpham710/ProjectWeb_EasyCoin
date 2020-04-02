namespace Easybook.Api.Core.Model.EasyWallet.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Wallet_Account_Reward")]
    public partial class Wallet_Account_Reward
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        [Key]
        public string ID { get; set; }
        public string Wallet_ID { get; set; }
        public decimal Reward_Amount { get; set; }
        public DateTime Createdate { get; set; }
        public DateTime Updatedate { get; set; }
        public string Remark { get; set; }
        public string CheckSumReward { get; set; }
    }
}
