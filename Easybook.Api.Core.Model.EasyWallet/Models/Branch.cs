namespace Easybook.Api.Core.Model.EasyWallet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Branch
    {
        [Key]
        public int Branch_ID { get; set; }

        public int Company_ID { get; set; }

        [Required]
        [StringLength(250)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Contact { get; set; }

        public DateTime Create_Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Create_User { get; set; }

        public DateTime Update_Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Update_User { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] Time_Stamp { get; set; }

        public int? Place_ID { get; set; }

        public int? SubPlace_ID { get; set; }

        public bool Show_State { get; set; }
    }
}
