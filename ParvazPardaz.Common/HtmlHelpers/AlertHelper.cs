using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParvazPardaz.Common.HtmlHelpers
{
    public static class AlertHelper
    {
        public enum AlertType
        {
            Success,
            Info,
            Warning,
            Danger
        }
        public static MvcHtmlString Alert(this HtmlHelper htmlHelper, AlertType alterType, string description, bool isValid = true, bool hasColseButton = false, string title = null)
        {
            var result = !isValid ? "<div  class=\"alert alert-" + alterType.ToString().ToLower() + " " + (hasColseButton ? " alert-dismissable" : null) + "\">" + (hasColseButton ? "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>" : null) + description + "<a style=\"float: left;\" href=\"#\" class=\"alert-link\">" + title + "</a></div>" : null;
            return new MvcHtmlString(result);
        }
    }
}