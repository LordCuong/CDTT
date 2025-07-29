using System.Web;
using System.Web.Mvc;

namespace LeHungCuong_ASP_CDTT
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
