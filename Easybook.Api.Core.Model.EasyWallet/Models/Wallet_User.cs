namespace Easybook.Api.Core.Model.EasyWallet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Wallet_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Wallet_User()
        {
            Transactions = new HashSet<Transaction>();
            Wallet_Account = new HashSet<Wallet_Account>();     
        }

        [Key]
        [StringLength(50)]
        public string Wallet_ID { get; set; }

        [Required]
        [StringLength(128)]
        public string User_ID { get; set; }

        [StringLength(128)]
        public string BlockChainID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wallet_Account> Wallet_Account { get; set; }
   
    }
}
