using System.Collections.Generic;
using System.Configuration;

namespace Easybook.Api.BusinessLogic.EasyWallet.Constant
{
    public class EwalletConstant
    {
        /// <summary>
        /// The merchant key
        /// </summary>
        public static string MerchantKey = "easybook";
        /// <summary>
        /// The merchant secret
        /// </summary>
        public static string WebserverKey = "Fn1BdQ3s";
        public static string keyAES = "q8IFDMqlrkChbf92/q+UQ6JUKcCg";
        public static string strWord = "^eb^";
        public static string StampserverKey = "Ane39274";
        public static string TokenEBW = "easybookewallet2019";
        public static string PushNotificationPath = "/push?api_key=";
        public static string TownBusSerectKey = "8y/B?E(H+MbQeThV";
        public static string TownBusIVKey = "UmXZEmidrOkg3L82";
        public static List<string> DeviceTokenLst = new List<string>();


        public static string SetMinTopUpMYR20FreeRides
        {
            get
            {
                return ConfigurationSettings.AppSettings["SetMinTopUpMYR20FreeRides"];

            }
        }
        public static string SecretPushNotificationApiKey
        {
            get
            {
                return  ConfigurationSettings.AppSettings["SecretPushNotificationApiKey"];
             
            }
        }
        public static string ApiPath
        {
            get
            {
                return ConfigurationSettings.AppSettings["ApiPath"];

            }
        }

        public static string MoengageAppID
        {
            get
            {
                return ConfigurationSettings.AppSettings["MoengageAppID"];

            }
        }
        public static string MoengageSecret
        {
            get
            {
                return ConfigurationSettings.AppSettings["MoengageSecret"];

            }
        }
        public static string MoengageApiPath
        {
            get
            {
                return ConfigurationSettings.AppSettings["MoengageApiPath"];

            }
        }
        public static List<string> Rewardstring
        {
            get
            {         
                List<string> result = new List<string>(ConfigurationSettings.AppSettings["Rewardstring"].Split('|'));
                return result;
            }
        }
        public static string CutoffDateInterest
        {
            get
            {
                return ConfigurationSettings.AppSettings["CutoffDateInterest"];
            }
        }
        public static string OldDailyRateInterest
        {
            get
            {
                return ConfigurationSettings.AppSettings["OldDailyRateInterest"];
            }
        }
        public static string OldYearRateInterest
        {
            get
            {
                return ConfigurationSettings.AppSettings["OldYearRateInterest"];
            }
        }
        public static string DateExpireReward
        {
            get
            {
                return ConfigurationSettings.AppSettings["DateExpireReward"];
            }
        }
      
        public static string EWalletPathPictureUpload
        {
            get
            {
                return ConfigurationSettings.AppSettings["EWalletPathPictureUpload"];
            }
        }
        public static int LimitTopupExtraPerUser
        {
            get
            {
                return int.Parse(ConfigurationSettings.AppSettings["LimitTopupExtraPerUser"]);
            }
        }
        public static string DateStartUpReward
        {
            get
            {
                return ConfigurationSettings.AppSettings["DateStartUpReward"];
            }
        }        
        public static decimal EWallet_LimitTopupAmount_USD
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitTopupAmount_USD"]);
            }
        }
        public static decimal EWallet_LimitTopupAmount_VND
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitTopupAmount_VND"]);
            }
        }
        public static decimal EWallet_LimitTopupAmount_MMK
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitTopupAmount_MMK"]);
            }
        }
        public static decimal EWallet_LimitTopupAmount_KHR
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitTopupAmount_KHR"]);
            }
        }
        public static decimal EWallet_LimitTopupAmount_Rp
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitTopupAmount_Rp"]);
            }
        }
        public static decimal EWallet_LimitTopupAmount_CNY
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitTopupAmount_CNY"]);
            }
        }
        public static decimal EWallet_LimitTopupAmount_MYR
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitTopupAmount_MYR"]);
            }
        }
        public static decimal EWallet_LimitTopupAmount_SGN
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitTopupAmount_SGN"]);
            }
        }
        public static decimal EWallet_LimitTopupAmount_THB
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitTopupAmount_THB"]);
            }
        }

        public static decimal EWallet_LimitWithdrawAmount_USD
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitWithdrawAmount_USD"]);
            }
        }
        public static decimal EWallet_LimitWithdrawAmount_VND
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitWithdrawAmount_VND"]);
            }
        }
        public static decimal EWallet_LimitWithdrawAmount_CNY
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitWithdrawAmount_CNY"]);
            }
        }
        public static decimal EWallet_LimitWithdrawAmount_MYR
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitWithdrawAmount_MYR"]);
            }
        }
        public static decimal EWallet_LimitWithdrawAmount_SGN
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitWithdrawAmount_SGN"]);
            }
        }
        public static decimal EWallet_LimitWithdrawAmount_THB
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitWithdrawAmount_THB"]);
            }
        }
        public static decimal EWallet_LimitWithdrawAmount_MMK
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitWithdrawAmount_MMK"]);
            }
        }

        public static decimal EWallet_LimitWithdrawAmount_KHR
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitWithdrawAmount_KHR"]);
            }
        }

        public static decimal EWallet_LimitWithdrawAmount_Rp
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitWithdrawAmount_Rp"]);
            }
        }

        public static decimal EWallet_LimitTopupMinimumAmount_USD
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitTopupMinimumAmount_USD"]);
            }
        }
        public static decimal EWallet_LimitTopupMinimumAmount_VND
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitTopupMinimumAmount_VND"]);
            }
        }
        public static decimal EWallet_LimitTopupMinimumAmount_CNY
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitTopupMinimumAmount_CNY"]);
            }
        }
        public static decimal EWallet_LimitTopupMinimumAmount_MYR
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitTopupMinimumAmount_MYR"]);
            }
        }
        public static decimal EWallet_LimitTopupMinimumAmount_SGN
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitTopupMinimumAmount_SGN"]);
            }
        }
        public static decimal EWallet_LimitTopupMinimumAmount_THB
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitTopupMinimumAmount_THB"]);
            }
        }
        public static decimal EWallet_LimitTopupMinimumAmount_MMK
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitTopupMinimumAmount_MMK"]);
            }
        }

        public static decimal EWallet_LimitTopupMinimumAmount_KHR
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitTopupMinimumAmount_KHR"]);
            }
        }

        public static decimal EWallet_LimitTopupMinimumAmount_Rp
        {
            get
            {
                return decimal.Parse(ConfigurationSettings.AppSettings["EWallet_LimitTopupMinimumAmount_Rp"]);
            }
        }
        public static int LimitNumberOfWithdrawalTimePerMonth
        {
            get
            {
                return int.Parse(ConfigurationSettings.AppSettings["LimitNumberOfWithdrawalTimePerMonth"]);
            }
        }

        public static string TOPUP = "TOPUP";
        public static string PAYMENT = "PAYMENT";
        public static string WITHDRAW = "WITHDRAW";
        public static string CONVERT = "CONVERT";
        public static string CASHBONUS = "CASHBONUS";
        public static string REWARD = "REWARD";
        public static string COMMISSION = "COMMISSION";
        public static string TOWNBUS = "TOWNBUS";
        public static string FULLTOWNBUS = "TOWNBUS-FULLROUTE";
        public static string SINGLETOWNBUS = "TOWNBUS-SINGLEROUTE";
        public static string FLATRATE = "FLAT RATE";
        public static string KM = "KM";
        public static string ZONE = "ZONE";
        //public static string IsProductionAPI { get { return System.Configuration.ConfigurationManager.AppSettings["EWallet_LimitAmount"]; } }
    }
}
