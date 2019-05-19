using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;


namespace ParvazPardaz.Common.HtmlHelpers
{
    public static class AutocompleteHelpers
    {
        public static MvcHtmlString AutocompleteBSFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> displayBindTo, string valueBindTo, string displayMember, string valueMember, string actionName, string controllerName, Dictionary<string, string> parameters, string value = null)
        {
            return AutocompleteBS(html, GetName(displayBindTo), valueBindTo, displayMember, valueMember, actionName, controllerName, parameters, value);
        }

        public static MvcHtmlString AutocompleteBS(this HtmlHelper html, string displayBindTo, string valueBindTo, string displayMember, string valueMember, string actionName, string controllerName, Dictionary<string, string> parameters = null, string value = null)
        {
            var autoCompletedConfig = parameters == null ? new AutoCompleteConfig() : new AutoCompleteConfig(parameters);
            AutocompleteScript(html, displayBindTo, valueBindTo, displayMember, valueMember, autoCompletedConfig.Params, actionName, controllerName);

            return MvcHtmlString.Create(html.TextBox(displayBindTo, value, new Dictionary<string, object>() { { "class", "form-control" } }).ToHtmlString());
        }

        public static MvcHtmlString AutocompleteBSFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> displayBindTo, string valueBindTo, string displayMember, string valueMember, string actionName, string controllerName, Dictionary<AutoCompleteKey, string> manualParameters, string value = null)
        {
            return AutocompleteBS(html, GetName(displayBindTo), valueBindTo, displayMember, valueMember, actionName, controllerName, manualParameters, value);
        }

        public static MvcHtmlString AutocompleteBS(this HtmlHelper html, string displayBindTo, string valueBindTo, string displayMember, string valueMember, string actionName, string controllerName, Dictionary<AutoCompleteKey, string> manualParameters = null, string value = null)
        {
            var autoCompletedConfig = manualParameters == null ? new AutoCompleteConfig() : new AutoCompleteConfig(manualParameters);
            AutocompleteScript(html, displayBindTo, valueBindTo, displayMember, valueMember, autoCompletedConfig.Params, actionName, controllerName);

            return MvcHtmlString.Create(html.TextBox(displayBindTo, value, new Dictionary<string, object>() { { "class", "form-control" } }).ToHtmlString());
        }

        internal static void AutocompleteScript(HtmlHelper html, string displayBindTo, string valueBindTo, string displayMember, string valueMember, Dictionary<string, string> parameters, string actionName = null, string controllerName = null)
        {
            string nav = "<nav style = \"position:absolute\"></nav>";

            if (html.ViewContext.Writer.ToString().Contains("<nav>"))
                nav = null;
            html.ViewContext.Writer.Write("<script>;var temp" + displayBindTo + "=null;" + "var " + displayBindTo + "datasource=null;" +
                        "$(document).ready(function () {" +
            "$(\"#" + displayBindTo + "\").focusin(function() {temp" + displayBindTo + "= $(\"#" + displayBindTo + "\").val()}).blur(function(){if($(\"#" + valueBindTo + "\").val()=='0' || $(\"#" + valueBindTo + "\").val()=='' || $(\"#" + displayBindTo + "\").val()!=temp" + displayBindTo + "){" + displayBindTo + "datasource=null;$(\"#" + displayBindTo + "\").val('');$(\"#" + valueBindTo + "\").val('');}else{$('#" + valueBindTo + "').trigger('change');$('#" + displayBindTo + "').trigger('change');}}).autocomplete({" +
                "select: function( event, ui ) {" +
                "$('#" + valueBindTo + "').val(ui.item.val);" +
                 displayBindTo + "datasource=ui.item.datasource;" +
                "$('#" + valueBindTo + "').trigger('change');" + "$('#" + displayBindTo + "').trigger('change');" +
                "temp" + displayBindTo + "= ui.item.value;" +

                "}," +
                "source: function (request, response) {" +
                    "$.ajax({" +
                        "url: \"/" + controllerName + "/" + actionName + "\"," +
                        "type: \"Get\"," +
                        "dataType: \"json\"," +
                        "data: { " + CreateStringParams(parameters) + " }," +
                        "success: function (data) {" +
                            "response($.map(data, function (item) {" +
                            "if(item!=null){" +
                                "return { label: item." + displayMember + ", value: item." + displayMember + ",val:item." + valueMember + ",datasource:item};" +
                            "}}))" +
                        "}" +
                    "})" +
                "}," +
                "messages: {" +
                    "noResults: \"\", results: \"\"" +
                "}" +
            "});" +
        "})" +
                "</script>" + nav);
        }

        private static string GetName<TModel, TProperty>(Expression<Func<TModel, TProperty>> exp)
        {
            MemberExpression body = exp.Body as MemberExpression;

            if (body == null)
            {
                UnaryExpression ubody = (UnaryExpression)exp.Body;
                body = ubody.Operand as MemberExpression;
            }

            return body.Member.Name;
        }

        private static string CreateStringParams(Dictionary<string, string> parameters)
        {
            return String.Join(",", parameters.ToList().Select(s => s.Key + ":" + s.Value).ToList());
        }
    }

    public class AutoCompleteConfig
    {
        public bool enable { get; set; }

        public Dictionary<string, string> Params { get; set; }

        public enum SettingControl
        {
            Manual,
            OnlyId,
            OnlyClass,
            Value
        }

        public AutoCompleteConfig()
            : this(true)
        {
        }

        public AutoCompleteConfig(bool enable)
        {
            this.enable = enable;
            if (enable)
            {
                Params = new Dictionary<string, string>();
                Params.Add("term", " request.term");
            }
        }

        public AutoCompleteConfig(Dictionary<string, string> parameters, SettingControl Control = SettingControl.OnlyId)
            : this(true)
        {
            switch (Control)
            {
                case SettingControl.Manual:
                    parameters.ToList().ForEach(f => Params.Add(f.Key, "$(\"" + f.Value + "\").val()"));
                    break;
                case SettingControl.OnlyId:
                    parameters.ToList().ForEach(f => Params.Add(f.Key, "$(\"#" + f.Value + "\").val()"));
                    break;
                case SettingControl.OnlyClass:
                    parameters.ToList().ForEach(f => Params.Add(f.Key, "$(\"." + f.Value + "\").val()"));
                    break;
                default:
                    parameters.ToList().ForEach(f => Params.Add(f.Key, f.Value));
                    break;
            }
        }

        public AutoCompleteConfig(Dictionary<AutoCompleteKey, string> parameters)
            : this(true)
        {
            foreach (var item in parameters)
            {
                switch (item.Key.SettingControl)
                {
                    case SettingControl.Manual:
                        Params.Add(item.Key.Key, "$(\"" + item.Value + "\").val()");
                        break;
                    case SettingControl.OnlyId:
                        Params.Add(item.Key.Key, "$(\"#" + item.Value + "\").val()");
                        break;
                    case SettingControl.OnlyClass:
                        Params.Add(item.Key.Key, "$(\"." + item.Value + "\").val()");
                        break;
                    default:
                        Params.Add(item.Key.Key, item.Value);
                        break;
                }
            }

        }

        public AutoCompleteConfig(Dictionary<string, string> parameters, string javascriptFunction)
            : this(true)
        {
            parameters.ToList().ForEach(f => Params.Add(f.Key, javascriptFunction + "(" + f.Value + ")"));
        }
    }

    public class AutoCompleteKey
    {
        public AutoCompleteConfig.SettingControl SettingControl { get; set; }
        public string Key { get; set; }
    }
}