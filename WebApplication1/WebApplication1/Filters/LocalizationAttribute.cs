using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Filters
{
    public class LocalizationAttribute : ActionFilterAttribute
    {
        private const string _defaultCulture = "en-US";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // 1. try get culture from URL
            string culture = (string)filterContext.RouteData.Values["culture"];

            if (string.IsNullOrEmpty(culture))
            {
                // 2. try get culture from user settings
                //if (user signed in)
                //{
                //    // get culture from user settings
                //}

                // - OR -

                // 2. if URL does not contain culture, then try get language from browser
                string[] languages = filterContext.RequestContext.HttpContext.Request.UserLanguages;
                if (languages.Length > 0)
                {
                    try
                    {
                        var c = new CultureInfo(languages[0]);
                        // if new culture info object success, then use the language as culture.
                        culture = languages[0];
                        filterContext.RouteData.Values.Add("culture", culture);
                    }
                    catch
                    {
                        // do nothing, just to verify if we can create a CultureInfo object from the language.
                    }
                }
            }

            if (!string.IsNullOrEmpty(culture) && culture != _defaultCulture)
            {
                try
                {
                    Thread.CurrentThread.CurrentCulture =
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

                    if (!filterContext.RouteData.Values.ContainsKey("culture"))
                    {
                        filterContext.RouteData.Values.Add("culture", culture);
                    }
                }
                catch (Exception e)
                {
                    throw new NotSupportedException(String.Format("ERROR: Invalid culture code '{0}'.", culture));
                }
            }
        }
    }
}