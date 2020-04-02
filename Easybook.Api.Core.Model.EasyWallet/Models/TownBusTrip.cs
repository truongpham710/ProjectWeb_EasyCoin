namespace Easybook.Api.Core.Model.EasyWallet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TownBusTrip")]
    public partial class TownBusTrip
    {
        [Key]
        public int TownBus_Trip_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Schedule_GUID { get; set; }

        public int? Company_ID { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        [Column(TypeName = "money")]
        public decimal? Max_Charge { get; set; }

        [StringLength(3)]
        public string Currency { get; set; }

        [StringLength(10)]
        public string Charge_Type { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Trip_Date_From { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Trip_Date_To { get; set; }

        [StringLength(5)]
        public string Country { get; set; }

        public int? Bus_ID { get; set; }

        [StringLength(500)]
        public string eWallet_Support { get; set; }

        [StringLength(50)]
        public string Create_User { get; set; }

        public DateTime? Create_Date { get; set; }

        [StringLength(50)]
        public string Update_User { get; set; }

        public DateTime? Update_Date { get; set; }

        [Column(TypeName = "money")]
        public decimal? Min_Charge { get; set; }

        public int? FreeRide { get; set; }
    }
}
