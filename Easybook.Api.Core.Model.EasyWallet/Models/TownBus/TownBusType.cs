namespace Easybook.Api.Core.Model.EasyWallet.Models.TownBus
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TownBusType")]
    public partial class TownBusType
    {
        [Key]
        public int Bus_ID { get; set; }

        [Required]
        [StringLength(10)]
        public string Bus_No { get; set; }

        public int Bus_Type { get; set; }

        public int Company_ID { get; set; }

        [StringLength(50)]
        public string Create_User { get; set; }

        public DateTime? Create_Date { get; set; }

        [StringLength(50)]
        public string Update_User { get; set; }

        public DateTime? Update_Date { get; set; }
    }
}
