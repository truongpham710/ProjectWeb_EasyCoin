using Easybook.Api.BusinessLogic.ApiLogic.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easybook.Api.BusinessLogic.MoengagePush
{
   public class MoengageResponse : Response
    {
        public string status { get; set; }
        public string responseId { get; set; }
        public string resquestId { get; set; }
        public string cid { get; set; }
    }
}
