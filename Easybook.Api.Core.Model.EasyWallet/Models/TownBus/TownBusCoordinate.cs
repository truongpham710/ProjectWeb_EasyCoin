namespace Easybook.Api.Core.Model.EasyWallet.Models.TownBus
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TownBusCoordinate
    {
        [Key]
        public int TownBus_Coordinate_ID { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longtitude { get; set; }

        [StringLength(50)]
        public string Create_User { get; set; }

        public DateTime? Create_Date { get; set; }

        [StringLength(50)]
        public string Update_User { get; set; }

        public DateTime? Update_Date { get; set; }

        [StringLength(500)]
        public string TownBus_Coordinate_Name { get; set; }

        public bool? Is_Station { get; set; }

        public decimal? Distance { get; set; }

        public bool? Is_Highway { get; set; }
    }
}
