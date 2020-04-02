using Easybook.Api.Core.Model.EasyWallet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easybook.Api.BusinessLogic.ApiLogic.BusinessObjects.Wallet
{
    public class TransactionTownBusResponse : Response
    {
        public TransactionTownBusResponse()
        {
            Triplst = new List<TranTownBus>();
        }
        public List<TranTownBus> Triplst { get; set; }
    }
    public class TranTownBus
    {
        public int ScanType { get; set; }
        public string ScanDateTime { get; set; }
        public string PassengerUsername { get; set; }
        public decimal ChargeAmout { get; set; }
        public string Currency { get; set; }

    }
    public class TokenTownBusResponse: Response
    {
        public int UserID { get; set; }
        public int CompanyID { get; set; }
        public string AccessToken { get; set; }
    }
    public class TownBusQRCode
    {
        public int TripId { get; set; }
        public string CarPlate { get; set; }
        public int CurrentCoordinationId { get; set; }
        public int NextCoordinationId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal DistanceSinceFirstStation { get; set; }
        public decimal DistanceToNextStation { get; set; }
        public decimal DistanceToLastStation { get; set; }
        public long Timestamp { get; set; }
    }
    public class ListEligibleTownBusesResponse : Response
    {
        public ListEligibleTownBusesResponse()
        {
            ListEligibleTownBuses = new List<Areas>();
        }
        
        public List<Areas> ListEligibleTownBuses { get; set; }
    }
    public class TownBusSupportedArea
    {
        public int TownBusType_Bus_ID { get; set; }
        public string TownBusType_Bus_No { get; set; }
    }
    public class Areas
    {
        public Areas()
        {
            TownBuslst = new List<TownBusSupportedArea>();
        }
        public string Area_Name { get; set; }
        public List<TownBusSupportedArea> TownBuslst { get; set; }
    }

    
}
