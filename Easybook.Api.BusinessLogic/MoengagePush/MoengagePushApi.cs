using Easybook.Api.BusinessLogic.EasyWallet.Constant;
using Easybook.Api.Core.CrossCutting.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Easybook.Api.BusinessLogic.MoengagePush
{
   public static class MoengagePushApi
    {
        public static string GetSignature(string app_id,string campaign_name,string api_secret)
        {
            return SimpleAesUtil.ComputeSha256Hash(app_id + "|" + campaign_name + "|" + api_secret);
        }
        public static MoengageResponse PushNotification (string notificationUniqueId,string message)
        {
            MoengageResponse moengageResponse = new MoengageResponse();


            MoengageRequest moengageRequest = new MoengageRequest();
            moengageRequest.appId = EwalletConstant.MoengageAppID;
            moengageRequest.campaignName = "DRIVER_ALERT";
            moengageRequest.signature = GetSignature(EwalletConstant.MoengageAppID, "DRIVER_ALERT", EwalletConstant.MoengageSecret);
            moengageRequest.requestType = "push";
         
            var targetPlatform = new List<string>();
            targetPlatform.Add("ANDROID");
            moengageRequest.targetPlatform = targetPlatform;
            moengageRequest.targetAudience = "User";

            TargetUserAttributes targetUserAttributes = new TargetUserAttributes();
            targetUserAttributes.attribute = "USER_ATTRIBUTE_UNIQUE_ID";
            targetUserAttributes.comparisonParameter = "is";
            targetUserAttributes.attributeValue = notificationUniqueId; // "f299b18a787a988";
            moengageRequest.targetUserAttributes = targetUserAttributes;
     
            PayLoad payLoad = new PayLoad();
            payLoad.ANDROID = new DiviceType();
            payLoad.ANDROID.title = "notification_title";
            payLoad.ANDROID.message = message;
            moengageRequest.payload = payLoad;

            CampaignDelivery campaignDelivery = new CampaignDelivery();
            campaignDelivery.type = "soon";
            moengageRequest.campaignDelivery = campaignDelivery;

            AdvancedSettings advancedSettings = new AdvancedSettings();
            advancedSettings.ttl = new Ttl();
            advancedSettings.ttl.ANDROID = "12";
            advancedSettings.ignoreFC = false;
            advancedSettings.sendAtHighPriority = true;
            moengageRequest.advancedSettings = advancedSettings;
            var request = new HttpRequestMessage(HttpMethod.Post, EwalletConstant.MoengageApiPath);
            string contentBody = JsonConvert.SerializeObject(moengageRequest);
            request.Content = new StringContent(contentBody, Encoding.UTF8, "application/json");
            try
            {
                using (HttpClient hclient = new HttpClient())
                {
                    var result = hclient.SendAsync(request).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        StreamReader reader = new StreamReader(result.Content.ReadAsStreamAsync().Result);
                        var text = reader.ReadToEnd();
                        moengageResponse = JsonConvert.DeserializeObject<MoengageResponse>(text);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return moengageResponse;
        }

    }
}
