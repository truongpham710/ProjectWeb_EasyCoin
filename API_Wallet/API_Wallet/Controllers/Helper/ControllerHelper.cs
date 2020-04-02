using API_Wallet.Models;
using Easybook.Api.BusinessLogic.ApiLogic.BusinessObjects.Wallet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace API_Wallet.Controllers.Helper
{
    /// <summary>
    /// VirtualController.
    /// </summary>
    public class VirtualController : Controller
    {
    }
    /// <summary>
    /// ControllerHelper.
    /// </summary>
    public static class ControllerHelper
    {

        /// <summary>
        /// GetPrivacyPolicyForWallet.
        /// </summary>
        public static PromotionContentResponse GetPromotionContenForWallet(string pcountry, string urlWallet="")
        {
            PromotionContentResponse pageContent = new PromotionContentResponse();

            var promotionContentModels = new PromotionContentModels(pcountry, urlWallet);

            string viewName = "~/Views/EasyWallet/_PromotionContent.cshtml";
            string strContent = RenderViewToString("CommonAgent", viewName, promotionContentModels);
            pageContent.HTMLPagePromotionContent = strContent;
            return pageContent;
        }
        /// <summary>
        /// RenderViewToString.
        /// </summary>
        public static string RenderViewToString(string controllerName, string viewName, object viewData, bool isOrderSummary = true)
        {
            try
            {
                using (var writer = new StringWriter())
                {
                    var routeData = new RouteData();
                    routeData.Values.Add("controller", controllerName);
                    var fakeControllerContext = new ControllerContext(new HttpContextWrapper(new HttpContext(new HttpRequest(null, "http://google.com", null), new HttpResponse(null))), routeData, new VirtualController());
                    var razorViewEngine = new RazorViewEngine();
                    var razorViewResult = razorViewEngine.FindView(fakeControllerContext, viewName, "", false);

                    var viewContext = new ViewContext(fakeControllerContext, razorViewResult.View, new ViewDataDictionary(viewData), new TempDataDictionary(), writer);
                    razorViewResult.View.Render(viewContext, writer);
                    return writer.ToString().Replace("/r/n", "").Replace("//\"", "");
                }
            }
            catch (Exception ex)
            {
            //    EmailUtil.SendEmail($"[Exception-RenderViewToString]-{viewName}-{ex.Message}", $"{ex.StackTrace}", EmailAddress.ProductOrderSummaryPersonInCharge);
                throw ex;
            }
        }
    }
}