using System.Collections.Generic;
using System.Configuration;

namespace Easybook.Api.DataAccessLayer
{
    public class EwalletConstant
    {
        public static List<string> Rewardstring
        {
            get
            {
                List<string> result = new List<string>(ConfigurationSettings.AppSettings["Rewardstring"].Split('|'));
                return result;
            }
        }
    }     
}