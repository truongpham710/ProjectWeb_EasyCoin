namespace Easybook.Api.Core.Model.EasyWallet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Currency
    {
       
        [Key]
        [StringLength(3)]
        public string Currency_Code { get; set; }

        [StringLength(50)]
        public string Currency_Name { get; set; }

    }
}
