namespace Easybook.Api.Core.Model.EasyWallet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Wallet_User = new HashSet<Wallet_User>();
        }

        [Key]
        public string User_ID { get; set; }

        public string User_Name { get; set; }

        [StringLength(900)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        [StringLength(150)]
        public string FirstName { get; set; }

        [StringLength(150)]
        public string LastName { get; set; }

        public DateTime? DOB { get; set; }

        [StringLength(50)]
        public string NRIC { get; set; }

        [StringLength(50)]
        public string Passport { get; set; }

        [StringLength(300)]
        public string Address { get; set; }

        [StringLength(50)]
        public string City { get; set; }
        
        public int Nationality { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(50)]
        public string Postal { get; set; }

        [StringLength(50)]
        public string GUID { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateUser { get; set; }

        public DateTime? UpdateDate { get; set; }

        [StringLength(50)]
        public string UpdateUser { get; set; }

        public bool? IsMemberAgent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wallet_User> Wallet_User { get; set; }
    }
}
