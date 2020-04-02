
using Easybook.Api.BusinessLogic.EasyWallet.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace API_Wallet.PushyMeLib
{
    /// <summary>
    /// SendNotification
    /// </summary>
    public static class SendNotification
    {
        /// <summary>
        /// SendSamplePush
        /// </summary>
        /// <param name="deviceTokens"></param>
        /// <param name="message"></param>
         /// <param name="CarPlate"></param>
        public static void PushNotification(List<string> deviceTokens, string message)
        {
            // Prepare array of target device tokens
           // List<string> deviceTokens = new List<string>();

            // Add your device tokens here
            //deviceTokens.Add("4dac95c75b78693ca8ce42");

            // Convert to string[] array
            string[] to = deviceTokens.ToArray();

            // Optionally, send to a publish/subscribe topic instead
            // string to = '/topics/news';

            // Set payload (it can be any object)
            var payload = new Dictionary<string, string>();

            // Add message parameter to dictionary
            payload.Add("message", message);

            // iOS notification fields
            var notification = new Dictionary<string, object>();

            notification.Add("badge", 1);
            notification.Add("sound", "ping.aiff");
            notification.Add("body", "Hello World \u270c");

            // Prepare the push HTTP request
            PushyPushRequest push = new PushyPushRequest(payload, to, notification);

            try
            {
                // Send the push notification
                PushyAPI.SendPush(push);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}