namespace Easybook.Api.Core.Model.EasyWallet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Wallet_Logs
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string AspNetUserId { get; set; }

        public int? ProductId { get; set; }

        [StringLength(128)]
        public string ApiCallerType { get; set; }

        [StringLength(128)]
        public string ApiMethod { get; set; }

        public string Request { get; set; }

        public string Exception { get; set; }

        public bool? Status { get; set; }

        public int? ReturnCode { get; set; }

        [StringLength(250)]
        public string MachineName { get; set; }

        [StringLength(100)]
        public string RemoteAddress { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
