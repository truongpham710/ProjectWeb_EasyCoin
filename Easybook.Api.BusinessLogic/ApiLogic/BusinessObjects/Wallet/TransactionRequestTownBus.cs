using System;
using System.ComponentModel.DataAnnotations;

namespace Easybook.Api.BusinessLogic.ApiLogic.BusinessObjects.Wallet
{
    public class TransactionRequestTownBus
    {
        public string Credential { get; set; }
        public string TranID { get; set; }
        public string User_ID { get; set; }
        public bool IsAutoCheckOut { get; set; } = false;
        public string Language { get; set; }

    }
    public class NotificationRequestTownBus
    {
        public int TripId {get;set;}
        public object TranID { get; set; }
        public object CarPlate { get; set; }
        public object User_ID { get; set; }
    }
    public class TokenRequestTownBus
    {

        [Required]
        public string Credential { get; set; }
        [Required]
        public string NotificationToken { get; set; } // Notification token for Puhsy 

        public string NotificationUniqueId { get; set; } = ""; // Unique Id for MoEngage
        public string CarPlate { get; set; }
    }
    public class TripsRequestTownBus
    {
        [Required]
        public string CarId { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public string StartDateTimeRange { get; set; }
        [Required]
        public string EndDateTimeRange { get; set; }

    }
}
