using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easybook.Api.Core.CrossCutting.Utility
{
    public class ConfigUtil
    {
        private static volatile ConfigUtil instance;
        private static object syncRoot = new Object();
        private static DateTime FileLastModifiedDate = DateTime.Now;
        private static CommonConfig _commonConfig;
        private static string path = Path.Combine(AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory, @"Config\CommonConfig.json");

        public static ConfigUtil Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConfigUtil();
                }

                return instance;
            }
        }

        public CommonConfig ReadConfig()
        {
            try
            {
                var currentLastWriteTime = new FileInfo(path).LastWriteTimeUtc;
                if (_commonConfig == null || currentLastWriteTime != FileLastModifiedDate)
                    RetrieveConfig();
            }
            catch (Exception ex)
            {
                if (_commonConfig == null) _commonConfig = new CommonConfig();
                //LogUtil.ErrorWithConditionalEmail(ex, $"[ConfigUtil]-[ReadConfig]", EmailAddress.EverythingPersonInCharge);
            }

            return _commonConfig;
        }

        public void RetrieveConfig()
        {
            try
            {
                lock (syncRoot)
                {
                    var currentLastWriteTime = new FileInfo(path).LastWriteTimeUtc;
                    if (_commonConfig != null && currentLastWriteTime == FileLastModifiedDate)
                        return;

                    if (File.Exists(path))
                    {
                        var appDomain = AppDomain.CurrentDomain;
                        string content = File.ReadAllText(path);
                         _commonConfig = JsonConvert.DeserializeObject<CommonConfig>(content, new JsonSerializerSettings{ DateFormatString = "yyyy-MM-dd" });
                        FileLastModifiedDate = currentLastWriteTime;
                    }
                }
            }
            catch (Exception ex)
            {
                if (_commonConfig == null) _commonConfig = new CommonConfig();
                //LogUtil.ErrorWithConditionalEmail(ex, $"[ConfigUtil]-[RetrieveConfig]", EmailAddress.EverythingPersonInCharge);
            }
        }
    }

    public class CommonConfig
    {
        public bool EnableTransactionReadUncommitted { get; set; } = false;
        public List<string> WebCrawlers { get; set; } = new List<string>();
        public List<ApiAgents> ApiAgents { get; set; } = new List<ApiAgents>();
        public int UrlEncodeMethod { get; set; } = 1;
        public bool EnableEcommerceConversionTrackingCode { get; set; } = false;
        public WebApiConfig WebApiConfig { get; set; } = new WebApiConfig();
        public bool EnableCommonDiscountCode { get; set; } = false;
        public PaymentSettings PaymentSettings { get; set; } = new PaymentSettings();
        public List<int> IsFeaturedTripCompanyId { get; set; } = new List<int>();
        public string TessaEngine { get; set; } = string.Empty; //SYTan added 2017-Aug-09
        public bool EnableTransferNote { get; set; } = false;

        public BusExternalSettings BusExternalSettings { get; set; }
        public string AgentSiteImagePath { get; set; } = string.Empty;
        public bool EnableMembershipTracking { get; set; } = false;
        public int BusOperatorScheduleDayLimit { get; set; } = 1;
        public bool IsChartisEnabled { get; set; } = false;
        public bool IsLazadaBannerEnabled { get; set; } = false;
        public bool IsInsuranceBannerEnabled { get; set; } = false;
        public MultiTierAgents MultiTierAgents { get; set; }
        public Dictionary<string, string> ExternalOperatorSettings { get; set; }
        public Dictionary<string, bool> DelayPaymentSettings { get; set; }
        public string BccEmailExceptThaiRoute { get; set; } = string.Empty;
        public string CompanyRequiredExtraPassengerInfo { get; set; } = string.Empty;
        public string CompanyRequiredExtraPassengerInfoWithPassport { get; set; } = string.Empty;
        public string CompanyRequireIcOrPassport { get; set; } = string.Empty;
        public string CompanyEnablePickupDropOffAddress { get; set; } = string.Empty;
        public BusDayPassExternalSettings BusDayPassExternalSettings { get; set; }
        public bool BusDayPassEnabled { get; set; } = false;
        public string MudahEcommerceConversionTrackingAccount { get; set; } = string.Empty;
        public int CaptchaLimit { get; set; } = 3;
        public int CaptchaLimitByIp { get; set; } = 5;

        public bool TrainTourEnabled { get; set; } = false;
        public List<string> GiftCardCodePrefixList { get; set; } = new List<string>();
        public bool EnableTrainJBWoodlandInsurance { get; set; } = true;
        public int GenerateOrderSummaryMethod { get; set; } = 1;
        public bool ApplyNewSeoMetaTagLogic { get; set; } = false;
        public DateTime RoundTripPromoStartDate { get; set; }
        public DateTime RoundTripPromoEndDate { get; set; }
        public DateTime EasiPointRedemptionPromoEndDate { get; set; }
        public int MembershipMethod { get; set; } = 1;
        public List<UserBookingRestrictions> UserBookingRestrictions = new List<UserBookingRestrictions>();

        public SearchIntellisensePopularPlace SearchIntellisensePopularPlaces { get; set; }

    }

    public class ApiAgents
    {
        public string AspNetUserId { get; set; }
        public decimal AlertCreditLimit { get; set; }
        public string RecipientEmailAddress { get; set; }
    }

    #region API Config
    public class WebApiConfig
    {
        public bool IsTestingEnvironment { get; set; } = false;
        public bool CheckBusCompanyAllowToBook { get; set; } = false;
        //public List<CountryDiscountInfo> CountryDiscountInfo { get; set; } = new List<CountryDiscountInfo>();
    }

    //public class CountryDiscountInfo
    //{
    //    public string Country { get; set; } = string.Empty;
    //    public List<ProductDiscount> ProductDiscount { get; set; } = new List<ProductDiscount>();
    //}

    //public class ProductDiscount
    //{
    //    public int ProductId { get; set; } = 0;
    //    public int DiscountPercentage { get; set; } = 0;
    //}

    #endregion

    public class PaymentSettings
    {
        public string Srimaju_AllianceBank_MID { get; set; } = string.Empty;
        public string Srimaju_AllianceBank_PXREF { get; set; } = string.Empty;
        public string Srimaju_AllianceBank_ReturnUrl { get; set; } = string.Empty;
        public string Srimaju_OCBCBank_MID { get; set; } = string.Empty;
        public string Srimaju_OCBCBank_TransactionPassword { get; set; } = string.Empty;
        public string Srimaju_OCBCBank_BPG_URL { get; set; } = string.Empty;
        public string Srimaju_OCBCBank_ReturnUrl { get; set; } = string.Empty;
        public string WTSNew_PayPal_SGD_Account { get; set; } = string.Empty;
        public string WTSNew_PayPal_SGD_Url { get; set; } = string.Empty;
        public string WTSNew_PayPal_SGD_CancelUrl { get; set; } = string.Empty;
        public string WTSNew_PayPal_SGD_NotifyUrl { get; set; } = string.Empty;
        public MolPay MolPay { get; set; } = new MolPay();
    }

    public class MultiTierAgents
    {
        public bool IsMultiTierAgentEnabled { get; set; } = false;
        public string BccEmail { get; set; } = string.Empty;
    }

    #region Bus External Settings
    public class BusExternalSettings
    {
        public RodaSettings RodaSettings { get; set; }
    }

    public class RodaSettings
    {
        public string Rqid { get; set; } = string.Empty;
        public string OpCode { get; set; } = string.Empty;
        public string UserAgent { get; set; } = string.Empty;
        public string WebApiUrl { get; set; } = string.Empty;
        public bool ShowTrip { get; set; } = true;
    }

    #endregion Bus External Settings

    #region BusDayPass External Settings
    public class BusDayPassExternalSettings
    {
        public KuraKuraSettings KuraKuraSettings { get; set; }
    }

    public class KuraKuraSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string AgentCode { get; set; } = string.Empty;
    }
    #endregion

    public class MolPay
    {
        public bool IsLive { get; set; } = true;
        public string Live_RequestUrl { get; set; } = string.Empty;
        public string Live_MerchantID { get; set; } = string.Empty;
        public string Live_VerifyKey { get; set; } = string.Empty;
        public string Live_SecretKey { get; set; } = string.Empty;
        public string Test_RequestUrl { get; set; } = string.Empty;
        public string Test_MerchantID { get; set; } = string.Empty;
        public string Test_VerifyKey { get; set; } = string.Empty;
        public string Test_SecretKey { get; set; } = string.Empty;
    }

    public class UserBookingRestrictions
    {
        public string AspNetUserId { get; set; }
        public string NotAllowedBookingCompanyId { get; set; }
    }

    public class SearchIntellisensePopularPlace
    {
        public bool EnableForBus { get; set; } = false;
        public bool EnableForTrain { get; set; } = false;
        public bool EnableForFerry { get; set; } = false;
    }
}
