namespace Easybook.Api.Core.Model.EasyWallet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Wallet_Checksum
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(50)]
        public string LastTranID { get; set; }

        [StringLength(50)]
        public string Snapshot_Checksum { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateUser { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}
