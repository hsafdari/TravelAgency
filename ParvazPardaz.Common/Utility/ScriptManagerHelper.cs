using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Common.Utility
{
    public class ScriptManagerHelper
    {
        #region Properties
        public HtmlHelper _htmlHelper { get; set; }
        #endregion

        #region Constractor
        internal ScriptManagerHelper() { }
        public ScriptManagerHelper(HtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        #endregion

        /// <summary>
        /// Format Script for textboxes
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public ScriptManagerHelper FormatScript()
        {
            _htmlHelper.ViewContext.Writer.Write("<script>function formatNumber (num,seprator) {return num.toString().replace(/(\\d)(?=(\\d{3})+(?!\\d))/g, \"$1\"+seprator+\"\");}</script>");
            return (ScriptManagerHelper)this;
        }
        /// <summary>
        /// convert textbox to numeric textbox
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public ScriptManagerHelper NumericScript()
        {
            _htmlHelper.ViewContext.Writer.Write("<script> $(document).ready(function () {" +
            "$(document).ready(function () {" +
                "$(\".IsNumeric\").keydown(function (e) {" +
                    "if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||" +
                        "(e.keyCode == 65 && e.ctrlKey === true) ||" +
                        "(e.keyCode >= 35 && e.keyCode <= 40)) {" +
                        "return;" +
                    "}" +
                    "if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {" +
                        "e.preventDefault();" +
                    "}" +
                "});" +
            "});});</script>");
            return (ScriptManagerHelper)this;
        }
        /// <summary>
        /// convert textbox to masked textbox
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public ScriptManagerHelper MaskScript()
        {
            _htmlHelper.ViewContext.Writer.Write("<script> $(document).ready(function () {$(\":input\").inputmask();});</script>");
            return (ScriptManagerHelper)this;
        }
        internal string GetAjaxScript(string functionName, string action, string controller, string actionScript, string parameters = null)
        {
            return "function " + functionName + "() {" +
                    "   $.ajax({" +
                    "      url:      '/" + controller + "/" + action + "'," +
                    "      method:   'GET'," +
                    "      dataType: 'json'," +
                    (parameters != null ? "data: { " + parameters.ToDictionary().CreateStringParams() + " }," : null) +
                    "      success:  function(series) {" +
                    "         var data = series;" +
                    actionScript + ";" +
                    "      }" +
                    "   });" +
                    "" +
                    "}";
        }
        internal string GetAjaxScript(string url, Dictionary<string, object> parameters = null)
        {
            return "function (e, data) {" +
                    "   $.ajax({" +
                    "      url:  " + url +
                    "      method:   'POST'," +
                    "      cache: false  , " +
                    "      dataType: 'json'," +
                    "      data :  " + (parameters != null ? parameters.CreateStringParams() : "null") + "," +
                    "      success:  function(data) {" +
                    "      };" +
                    "   });" +
                    "" +
                    "}";
        }
        public override string ToString()
        {
            return null;
        }
    }
}
