namespace Easybook.Api.Core.Model.EasyWallet.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User_CreditCard")]
    public partial class User_Card
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        [Key]
        public string ID { get; set; }
        public string Card_HolderName { get; set; }      
        public string Card_Number { get; set; }
        public string Card_Type { get; set; }
        public string Card_Country { get; set; }
        public string Card_ExpireDate { get; set; }
        public string Currency_Code { get; set; }      
        public string Bank_Name { get; set; }     
        public DateTime Create_date { get; set; }
        public DateTime Update_date { get; set; }
        public string User_ID { get; set; }
    }
}
