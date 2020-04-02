using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easybook.Api.BusinessLogic.ApiLogic.BusinessObjects.Wallet
{
   public class CalculationWithdrawalFeesRequest
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string CurrencyCode { get; set; }
        [Required]
        public decimal WithdrawAmount { get; set; }

    }
}
