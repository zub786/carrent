using System.Web;
using System.Web.Mvc;

namespace CarRentManagementSystem_DomainClasses
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
