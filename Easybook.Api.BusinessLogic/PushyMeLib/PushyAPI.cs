using Easybook.Api.BusinessLogic.EasyWallet.Constant;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace API_Wallet.PushyMeLib
{
    /// <summary>
    /// PushyAPI
    /// </summary>
    public class PushyAPI
    {
        /// <summary>
        /// SendPush
        /// </summary>
        /// <param name="push"></param>
        public static void SendPush(PushyPushRequest push)
        {
            // Create an HTTP request to the Pushy API
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(EwalletConstant.ApiPath + EwalletConstant.PushNotificationPath+ EwalletConstant.SecretPushNotificationApiKey);

            // Send a JSON content-type header
            request.ContentType = "application/json";

            // Set request method to POST
            request.Method = "POST";

            // Convert request post body to JSON (using JSON.NET package from Nuget)
            string postData = JsonConvert.SerializeObject(push);

            // Convert post data to a byte array
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentLength property of the WebRequest
            request.ContentLength = byteArray.Length;

            // Get the request stream
            Stream dataStream = request.GetRequestStream();

            // Write the data to the request stream
            dataStream.Write(byteArray, 0, byteArray.Length);

            // Close the stream
            dataStream.Close();

            // Proceed with caution
            WebResponse response;

            try
            {
                // Execute the request
                response = request.GetResponse();
            }
            catch (WebException exc)
            {
                // Get returned JSON error as string
                string errorJSON = new StreamReader(exc.Response.GetResponseStream()).ReadToEnd();

                // Parse into object
                PushyAPIError error = JsonConvert.DeserializeObject<PushyAPIError>(errorJSON);

                // Throw error
                throw new Exception(error.error);
            }

            // Open the stream using a StreamReader for easy access
            StreamReader reader = new StreamReader(response.GetResponseStream());

            // Read the response JSON for debugging
            string responseData = reader.ReadToEnd();

            // Clean up the streams
            reader.Close();
            response.Close();
            dataStream.Close();
        }
    }
    /// <summary>
    /// PushyPushRequest
    /// </summary>
    public class PushyPushRequest
    {
        /// <summary>
        /// to
        /// </summary>
        public object to;
        /// <summary>
        /// data
        /// </summary>
        public object data;
        /// <summary>
        /// notification
        /// </summary>
        public object notification;

        /// <summary>
        /// PushyPushRequest
        /// </summary>
        /// <param name="data"></param>
        /// <param name="to"></param>
        /// <param name="notification"></param>
        public PushyPushRequest(object data, object to, object notification)
        {
            this.to = to;
            this.data = data;
           // this.notification = notification;
        }
    }
    /// <summary>
    /// PushyAPIError
    /// </summary>
    public class PushyAPIError
    {
        /// <summary>
        /// error
        /// </summary>
        public string error;

        /// <summary>
        /// PushyAPIError
        /// </summary>
        /// <param name="error"></param>
        public PushyAPIError(string error)
        {
            this.error = error;
        }
    }
}