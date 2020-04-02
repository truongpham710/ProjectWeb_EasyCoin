namespace Easybook.Api.Core.Model.EasyWallet.Models.TownBus
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TownBusCompany
    {
        [Key]
        public int TownBus_Company_ID { get; set; }

        public int Company_ID { get; set; }

        public bool? Status { get; set; }

        [StringLength(50)]
        public string Create_User { get; set; }

        public DateTime? Create_Date { get; set; }

        [StringLength(50)]
        public string Update_User { get; set; }

        public DateTime? Update_Date { get; set; }
    }
}
