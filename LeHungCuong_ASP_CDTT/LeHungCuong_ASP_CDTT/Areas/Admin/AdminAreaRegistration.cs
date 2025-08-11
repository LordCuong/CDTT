using System.Web.Mvc;

namespace LeHungCuong_ASP_CDTT.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces : new [] { "LeHungCuong_ASP_CDTT.Areas.Admin.Controllers" } // Specify the namespace for the controllers in this area
            );
        }
    }
}