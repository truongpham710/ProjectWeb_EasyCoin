using Easybook.Api.Core.Model.EasyWallet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easybook.Api.BusinessLogic.ApiLogic.BusinessObjects.Wallet
{
        public class UserBankAccountRequest
        {      
        public string ID { get; set; }
        public string Verify { get; set; }
        public string CountryBank { get; set; }

        public string BankCurrency { get; set; }

        public string BankName { get; set; }

        public string AccountName { get; set; }

        public string AccountNumber { get; set; }    

        public string urlBankAcc { get; set; }

        public string urlPassIC { get; set; }    

        public string User_ID { get; set; }
        public string Comments  { get; set; }
        public string BankCity { get; set; }     
        public string BranchName { get; set; }     
        public string BranchCode { get; set; }
        public string FileNameBankAcc { get; set; }
        public string FileNamePasIC { get; set; }
    }

    public class ListUserBankAccountReponse : Response
    {
        public List<User_Bank_Account> lstUserBankAcc { get; set; }

    }
}
