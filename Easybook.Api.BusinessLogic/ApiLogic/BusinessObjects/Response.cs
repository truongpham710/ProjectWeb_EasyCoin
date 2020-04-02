using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easybook.Api.BusinessLogic.ApiLogic.BusinessObjects
{
    public abstract class Response
    {
        /// <summary>
        /// 1: PASSED 
        /// 0: FAILED
        /// </summary>
        [Required]
        public int Status { get; set; } = 0;

        /// <summary>
        /// RETURN CODE
        /// Refer to Documentation for Return Code
        /// </summary>
        [Required]
        public int Code { get; set; } = 0;

        /// <summary>
        /// RETURN MESSAGE
        /// Refer to Documentation for Return Code Message
        /// </summary>
        [Required]
        public string Message { get; set; } = "";
    }
}
