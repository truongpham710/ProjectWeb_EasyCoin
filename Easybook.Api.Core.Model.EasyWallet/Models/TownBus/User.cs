namespace Easybook.Api.Core.Model.EasyWallet.Models.TownBus
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [Key]
        public int User_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Login_ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int Branch_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public DateTime LastLogin_Date { get; set; }

        public DateTime Create_Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Create_User { get; set; }

        public DateTime Update_Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Update_User { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] Time_Stamp { get; set; }

        public int From_CompanyID { get; set; }

        [Column(TypeName = "money")]
        public decimal? Commission { get; set; }

        [StringLength(1)]
        public string Commission_Type { get; set; }

        public bool? ChangePassword { get; set; }

        public int? FailedPasswordCount { get; set; }

        public bool? IsLockedOut { get; set; }

        public DateTime? LockedOutDate { get; set; }

        public int? Main_Agent_ID { get; set; }

        [StringLength(500)]
        public string AccessRight { get; set; }

        [Required]
        [StringLength(1)]
        public string Is_Peon { get; set; }

        [Required]
        [StringLength(64)]
        public string Cash_Agent_Type { get; set; }

        [Required]
        [StringLength(4)]
        public string Status { get; set; }

        [Column(TypeName = "money")]
        public decimal CurrentCreditLimit { get; set; }

        public bool CreditLimitEnabled { get; set; }

        [StringLength(20)]
        public string IC { get; set; }

        [StringLength(20)]
        public string Contact { get; set; }

        public DateTime? Date_Join { get; set; }

        public DateTime? Date_Left { get; set; }

        [StringLength(50)]
        public string ExternalLogin_ID { get; set; }

        public DateTime? Password_ExpiryDate { get; set; }

        public string ReprintTicketKey { get; set; }

        public bool EnableReprintTicket { get; set; }

        public bool ReprintTicketLock { get; set; }

        public bool? Is_InternalEB { get; set; }

        [StringLength(1000)]
        public string FeatureAccess { get; set; }

        public int? CommTemplate_ID { get; set; }

        public int? AgentVisibleRouteTemplate_ID { get; set; }

        public bool EIMAOnly { get; set; }

        public DateTime? LastActivityDate { get; set; }

        public bool? ForcedLogout { get; set; }
    }
}
