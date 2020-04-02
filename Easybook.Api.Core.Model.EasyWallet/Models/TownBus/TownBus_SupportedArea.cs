namespace Easybook.Api.Core.Model.EasyWallet.Models.TownBus
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TownBus_SupportedArea
    {
        public int ID { get; set; }

        public int? Company_ID { get; set; }

        public int? TownBusType_Bus_ID { get; set; }

        [StringLength(50)]
        public string TownBusType_Bus_No { get; set; }

        [StringLength(200)]
        public string Area_Name { get; set; }

        [StringLength(1)]
        public string Status { get; set; }

        public DateTime? Create_Date { get; set; }

        [StringLength(50)]
        public string Create_By { get; set; }

        public DateTime? Update_Date { get; set; }

        [StringLength(50)]
        public string Update_By { get; set; }
    }
}
