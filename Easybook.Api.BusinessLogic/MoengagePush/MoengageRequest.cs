using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easybook.Api.BusinessLogic.MoengagePush
{
   public class MoengageRequest
    {
        public string appId { get; set; }
        public string campaignName { get; set; }
        public string signature { get; set; }
        public string requestType { get; set; }
        public List<string> targetPlatform { get; set; }
        public string targetAudience { get; set; }
   
        public TargetUserAttributes targetUserAttributes { get; set; }
        public PayLoad payload { get; set; }
        public CampaignDelivery campaignDelivery { get; set; }
        public AdvancedSettings advancedSettings { get; set; }

    }
    public class TargetUserAttributes
    {
        public string attribute { get; set; }
        public string comparisonParameter { get; set; }
        public string attributeValue { get; set; }
    }
    public class PayLoad
    {
        public DiviceType ANDROID { get; set; }
    }
    public class DiviceType
    {
        public string message { get; set; }
        public string title { get; set; }

       // public List<AdditionalActions> additionalActions { get; set; }
    }
    public class AdditionalActions
    {
        public string type { get; set; }
        public string value { get; set; }
        public string name { get; set; }
        public string iconURL { get; set; }
    }
    public class CampaignDelivery
    {
        public string type { get; set; }
    }
    public class AdvancedSettings
    {
        public Ttl ttl { get; set; }
        public bool ignoreFC { get; set; }
        public bool sendAtHighPriority { get; set; }
    }
    public class Ttl
    {
        public string ANDROID { get; set; }
    }
}
