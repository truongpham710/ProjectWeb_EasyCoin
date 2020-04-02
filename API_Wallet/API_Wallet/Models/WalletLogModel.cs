using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Wallet.Models
{
    public class WalletLogModel
    {       
            public int Id { get; set; }
        
            public string AspNetUserId { get; set; }

            public int? ProductId { get; set; }

            public string ApiCallerType { get; set; }

            public string ApiMethod { get; set; }

            public string Request { get; set; }

            public string Exception { get; set; }

            public bool? Status { get; set; }

            public int? ReturnCode { get; set; }

            public string MachineName { get; set; }
      
            public string RemoteAddress { get; set; }

            public DateTime? CreateDate { get; set; }       
    }
}