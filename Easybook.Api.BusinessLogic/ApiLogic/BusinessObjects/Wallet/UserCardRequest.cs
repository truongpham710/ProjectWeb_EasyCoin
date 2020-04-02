using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easybook.Api.BusinessLogic.ApiLogic.BusinessObjects.Wallet
{
        public class UserCardRequest
        {   
            public string Card_HolderName { get; set; }
            public string Card_Number { get; set; }
            public string Card_Type { get; set; }
            public string Card_ExpireDate { get; set; }
            public string Currency_Code { get; set; }
            public string Bank_Name { get; set; }
            public string User_ID { get; set; }
            public string Card_Country { get; set; }
    }
}
