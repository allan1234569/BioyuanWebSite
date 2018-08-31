using System.Web.Mvc;

namespace MvcApplication1.Areas.Admin
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
                "Admin_route1",
                "Admin/{controller}/ProductsManage/{action}",
                namespaces: new string[] { "MvcApplication1.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_route2",
                "Admin/{controller}/UserManage/{action}",
                namespaces: new string[] { "MvcApplication1.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_route3",
                "Admin/{controller}/ProductCategoryManage/{action}",
                namespaces: new string[] { "MvcApplication1.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_route4",
                "Admin/{controller}/IntroductionManage/{action}",
                namespaces: new string[] { "MvcApplication1.Areas.Admin.Controllers" }
            );


            context.MapRoute(
                "Admin_route5",
                "Admin/{controller}/NewsManage/{action}",
                namespaces: new string[] { "MvcApplication1.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_route6",
                "Admin/{controller}/NewsCategoryManage/{action}",
                namespaces: new string[] { "MvcApplication1.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "MvcApplication1.Areas.Admin.Controllers" }
            );
        }
    }
}
