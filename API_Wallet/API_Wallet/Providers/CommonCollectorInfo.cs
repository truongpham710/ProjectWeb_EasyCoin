namespace API_Wallet.Providers
{
    public class CommonCollectorInfo
    {
        /// <summary>
        /// Collector salutation 'Mr' or 'Ms'
        /// </summary>      
        public string Salutation { get; set; }

        /// <summary>
        /// Collector name
        /// </summary>
      
        public string Name { get; set; } = "";

        /// <summary>
        /// Collector email
        /// </summary>
       
        public string Email { get; set; } = "";

        //public string PhoneCountryCode { get; set; } = "";

        /// <summary>
        /// Contact number of collector
        /// </summary>
     
        public string ContactNumber { get; set; } = "";

        /// <summary>
        /// Nationality - ISO ALPHA-2 Code. eg. 'MY' for Malaysia
        /// </summary>
   
        public string Nationality { get; set; } = "";
    }
}