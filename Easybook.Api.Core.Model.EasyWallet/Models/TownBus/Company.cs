namespace Easybook.Api.Core.Model.EasyWallet.Models.TownBus
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Company
    {
        [Key]
        public int Company_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Company_Name { get; set; }

        public int GoInternet { get; set; }

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

        public int? Tour_Company_ID { get; set; }

        [StringLength(50)]
        public string Company_Path { get; set; }

        [StringLength(2000)]
        public string Notice { get; set; }

        public int? Company_Rank { get; set; }

        public bool? Separate_Boarding_Code { get; set; }

        [Required]
        [StringLength(50)]
        public string DataSource { get; set; }

        [Required]
        [StringLength(255)]
        public string LogoFileName { get; set; }

        [StringLength(100)]
        public string AvailableCurrency { get; set; }

        public bool? KioskVersion { get; set; }

        public bool? FS3Version { get; set; }

        public bool? ShowCounterDisplay { get; set; }

        public bool? ShowOnlineDisplay { get; set; }

        public bool? DiffOnlinePrice { get; set; }

        [StringLength(2)]
        public string Country { get; set; }

        public bool? Membership { get; set; }

        [StringLength(10)]
        public string Company_Code { get; set; }

        public bool DisplayCompanyLogoOnEBKHeader { get; set; }

        public int? CarCompanyId { get; set; }

        public bool ShowOnlineSurcharge { get; set; }

        public int Max_Trips { get; set; }

        public int Max_Trips_Period_In_Minutes { get; set; }

        public int Max_Trips_Per_Coach { get; set; }

        public int Max_Trips_Per_Coach_Period_In_Minutes { get; set; }
    }
}
