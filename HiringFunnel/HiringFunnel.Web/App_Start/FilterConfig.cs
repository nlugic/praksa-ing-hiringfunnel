using System.Web.Mvc;

namespace HiringFunnel.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // odbija https iz nekog razloga
            //filters.Add(new RequireHttpsAttribute()); // da li je ovo dovoljno za sigurnost?
        }
    }
}
