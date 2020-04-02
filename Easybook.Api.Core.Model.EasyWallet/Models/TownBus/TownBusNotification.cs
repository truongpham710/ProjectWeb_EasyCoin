namespace Easybook.Api.Core.Model.EasyWallet.Models.TownBus
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TownBusNotification")]
    public partial class TownBusNotification
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int? CompanyID { get; set; }

        public int? BusID { get; set; }

        [StringLength(50)]
        public string NotificationToken { get; set; }

        [StringLength(50)]
        public string NotificationUniqueId { get; set; }

        [StringLength(10)]
        public string CarPlate { get; set; }
    }
}
