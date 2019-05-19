using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.Common.HtmlHelpers
{
    public static class GetResource
    {
        public static string MyResource<T>(this HtmlHelper html, object key)
        {
            return new ResourceManager(typeof(T)).GetString(key.ToString());
        }
    }
}
