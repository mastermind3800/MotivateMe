namespace MotivateMe.Web
{
    using System.Web.Mvc;

    public class ViewEnginesConfiguration
    {
        internal static void RegisterViewEngines(System.Web.Mvc.ViewEngineCollection viewEngineCollection)
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}
