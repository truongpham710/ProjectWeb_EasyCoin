using System.Web;
using System.Web.Mvc;

namespace ProjectWeb_EasyCoin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
