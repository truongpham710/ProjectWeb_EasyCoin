using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Wallet.Models
{
    /// <summary>
    /// PromotionContentModels
    /// </summary>
    public class PromotionContentModels
    {
        /// <summary>
        /// Language
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// UrlWalet
        /// </summary>
        public string UrlWalet { get; set; }
        /// <summary>
        /// PromotionContentModels
        /// </summary>
        public PromotionContentModels(string country, string urlWalet = "")
        {

            Country = country;
            UrlWalet = urlWalet;

        }
    }
}