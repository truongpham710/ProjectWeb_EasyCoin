using System.ComponentModel.DataAnnotations;

namespace Easybook.Api.Core.Model.EasyWallet.Models
{
    public partial class Wallet_Interest
    {
        [Key]
        public string Country_Code { get; set; }
        public string Interest_day { get; set; }
        public string Interest_month { get; set; }
        public string Interest_year { get; set; }
        public string Remark { get; set; }    
    }
}
