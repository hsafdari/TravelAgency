using ParvazPardaz.Common.HtmlHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages;

namespace ParvazPardaz.Common.Utility
{
    public class RenderScriptHelper
    {
        #region Properties
        private HtmlHelper htmlHelper { get; set; }
        #endregion

        #region Contructor
        public RenderScriptHelper(HtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
        }
        #endregion

        #region Methods
        public RenderScriptHelper Script(Func<object, HelperResult> template)
        {
            htmlHelper.ViewContext.HttpContext.Items["_script_" + Guid.NewGuid()] = template;
            return (RenderScriptHelper)this;
        }
        public RenderScriptHelper Script(string template)
        {
            htmlHelper.ViewContext.HttpContext.Items["_script_" + Guid.NewGuid()] = template;
            return (RenderScriptHelper)this;
        }
        public RenderScriptHelper Section()
        {
            foreach (object key in htmlHelper.ViewContext.HttpContext.Items.Keys)
            {
                if (key.ToString().StartsWith("_script_"))
                {
                    if (htmlHelper.ViewContext.HttpContext.Items[key] is Func<object, HelperResult>)
                    {
                        var template = htmlHelper.ViewContext.HttpContext.Items[key] as Func<object, HelperResult>;
                        if (template != null)
                        {
                            htmlHelper.ViewContext.Writer.Write(template(null));
                        }
                    }
                    if (htmlHelper.ViewContext.HttpContext.Items[key] is string)
                    {
                        var template = htmlHelper.ViewContext.HttpContext.Items[key] as string;
                        if (template != null)
                        {
                            htmlHelper.ViewContext.Writer.Write(template);
                        }
                    }
                }
            }
            return (RenderScriptHelper)this;
        }
        public override string ToString()
        {
            return null;
        }

        #endregion
    }
}
