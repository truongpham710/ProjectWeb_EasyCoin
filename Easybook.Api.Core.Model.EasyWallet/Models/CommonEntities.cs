namespace Easybook.Api.Core.Model.EasyWallet.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CommonEntities : DbContext
    {
        public CommonEntities()
            : base("name=CommonEntities")
        {
        }

        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }     

     
    }
}
