namespace Easybook.Api.Core.Model.EasyWallet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TownBusRoute")]
    public partial class TownBusRoute
    {
        [Key]
        public int TownBus_Route_ID { get; set; }

        public int TownBus_Trip_ID { get; set; }

        public int Company_ID { get; set; }

        public int TownBus_Coordinate_ID { get; set; }

        public int? Sequence { get; set; }

        [StringLength(50)]
        public string Create_User { get; set; }

        public DateTime? Create_Date { get; set; }

        [StringLength(50)]
        public string Update_User { get; set; }

        public DateTime? Update_Date { get; set; }
    }
}
