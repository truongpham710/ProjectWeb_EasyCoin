namespace Easybook.Api.Core.Model.EasyWallet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User_Bank_Account")]
    public partial class User_Bank_Account
    {
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        public string CountryBank { get; set; }

        [StringLength(50)]
        public string BankCurrency { get; set; }

        [StringLength(200)]
        public string BankName { get; set; }

        [StringLength(100)]
        public string AccountName { get; set; }

        [StringLength(200)]
        public string AccountNumber { get; set; }

        [StringLength(10)]
        public string Verify { get; set; }

        [StringLength(200)]
        public string urlBankAcc { get; set; }

        [StringLength(200)]
        public string urlPassIC { get; set; }

        public DateTime? Create_date { get; set; }

        public DateTime? Update_date { get; set; }

        [StringLength(50)]
        public string User_ID { get; set; }

        [StringLength(500)]
        public string Comments { get; set; }

        [StringLength(100)]
        public string BankCity { get; set; }

        [StringLength(100)]
        public string BranchName { get; set; }

        [StringLength(100)]
        public string BranchCode { get; set; }

    }
}
