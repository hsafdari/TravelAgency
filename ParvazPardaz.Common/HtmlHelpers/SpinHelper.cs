using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Common.HtmlHelpers
{
    public static class SpinHelper
    {
        public static Spin Spin(this HtmlHelper htmlHelper, string name, object htmlAttribute = null)
        {
            return new Spin(htmlHelper, name, htmlAttribute);
        }
        public static SpinFor<TModel, TProperty> SpinFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttribute = null)
        {
            return new SpinFor<TModel, TProperty>(htmlHelper, expression, htmlAttribute);
        }
    }

    public class Spin : BaseComponent
    {
        public Spin(HtmlHelper htmlHelper, string name, object htmlAttribute = null)
        {
            base.name = name;
            base.htmlHelper = htmlHelper;
            base.htmlAttribute = htmlAttribute;
            min = null;
            max = null;

        }

        private int? min { get; set; }
        private int? max { get; set; }
        private string postfix { get; set; }
        private string prefix { get; set; }
        private bool callConfigWithId { get; set; }
        private bool callConfigWithClass { get; set; }
        private string classElement { get; set; }
        private bool verticalbutton { get; set; }
        private bool configScript { get; set; }

        public Spin Limit(int min, int max)
        {
            this.min = min;
            this.max = max;
            return (Spin)this;
        }

        public Spin PreFix(string prefix)
        {
            this.prefix = prefix;
            return (Spin)this;
        }

        public Spin Postfix(string postfix)
        {
            this.postfix = postfix;
            return this;
        }

        public Spin VerticalButton(bool verticalbutton)
        {
            this.postfix = postfix;
            return this;
        }
        public Spin CallConfigWithId(bool callConfigWithId)
        {
            this.callConfigWithId = callConfigWithId;
            return this;
        }
        public Spin CallConfigWithClass(bool callConfigWithClass, string classElement)
        {
            this.callConfigWithClass = callConfigWithClass;
            this.classElement = classElement;
            return this;
        }
        public Spin ConfigScript(bool configScript)
        {
            this.configScript = configScript;
            return this;
        }
        public override string ToString()
        {
            if (configScript)
                ConfigScript();
            return System.Web.Mvc.Html.InputExtensions.TextBox(this.htmlHelper, base.name, null, htmlAttribute).ToHtmlString();
        }

        private void ConfigScript()
        {
            this.htmlHelper.ViewContext.Writer.Write("<script type=\"text/javascript\"> " +
                                                       "$(document).ready(function () { " +
                                                       "$('" + (this.callConfigWithClass == false ? "#" : ".") + (this.callConfigWithId && htmlAttribute.ToDictionary().Any(a => a.Key.Equals("id")) ? htmlAttribute.ToDictionary().FirstOrDefault(a => a.Key.Equals("id")).Value.ToString() : (this.callConfigWithClass) ? this.classElement : base.name) + "').TouchSpin({ " +
                                                          (min != null ? "min:" + min + "," : null) +
                                                          (max != null ? "max: " + max + "," : null) +
                //   stepinterval: 50,                     
                                                          (this.postfix != null ? "postfix: '" + postfix + "'," : null) +
                                                          (this.prefix != null ? "prefix: '" + prefix + "'," : null) +
                                                          (this.verticalbutton ? "verticalbuttons: true," : null) +
                                                      "});" +
                                                      "})" +
                                                      "</script>"
                                                      );

        }

    }




    public class SpinFor<TModel, TProperty> : BaseComponent
    {
        public SpinFor(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttribute = null)
        {
            this.htmlHelper = htmlHelper;
            this.expression = expression;
            this.htmlAttribute = htmlAttribute;
            this.configScript = true;
            this.callConfigWithClass = false;
            base.name = expression.MemberWithoutInstance();
            min = null;
            max = null;

        }
        //public object htmlAttribute { get; set; }
        //public HtmlHelper<TModel> htmlHelper { get; set; }
        public Expression<Func<TModel, TProperty>> expression { get; set; }
        private int? min { get; set; }
        private int? max { get; set; }
        private string postfix { get; set; }
        private string prefix { get; set; }
        private bool callConfigWithId { get; set; }
        private bool callConfigWithClass { get; set; }
        private string classElement { get; set; }
        private bool verticalbutton { get; set; }
        private bool configScript { get; set; }

        public SpinFor<TModel, TProperty> Limit(int min, int max)
        {
            this.min = min;
            this.max = max;
            return this;
        }


        public SpinFor<TModel, TProperty> PreFix(string prefix)
        {
            this.prefix = prefix;
            return this;
        }

        public SpinFor<TModel, TProperty> Postfix(string postfix)
        {
            this.postfix = postfix;
            return this;
        }

        public SpinFor<TModel, TProperty> VerticalButton(bool verticalbutton)
        {
            this.postfix = postfix;
            return this;
        }
        public SpinFor<TModel, TProperty> CallConfigWithId(bool callConfigWithId)
        {
            this.callConfigWithId = callConfigWithId;
            return this;
        }
        public SpinFor<TModel, TProperty> CallConfigWithClass(bool callConfigWithClass, string classElement)
        {
            this.callConfigWithClass = callConfigWithClass;
            this.classElement = classElement;
            return this;
        }
        public SpinFor<TModel, TProperty> ConfigScript(bool configScript)
        {
            this.configScript = configScript;
            return this;
        }
        public override string ToString()
        {
            //var s = System.Web.Mvc.Html.InputExtensions.TextBox(base.htmlHelper, base.name, null, htmlAttribute).ToHtmlString();

            if (configScript)
                ConfigScript();
            return System.Web.Mvc.Html.InputExtensions.TextBoxFor(this.htmlHelper as HtmlHelper<TModel>, expression, htmlAttribute).ToHtmlString();
        }

        private void ConfigScript()
        {
            this.htmlHelper.ViewContext.Writer.Write("<script type=\"text/javascript\"> " +
                                                       "$(document).ready(function () { " +
                                                       "$('" + (this.callConfigWithClass == false ? "#" : ".") + (this.callConfigWithId && htmlAttribute.ToDictionary().Any(a => a.Key.Equals("id")) ? htmlAttribute.ToDictionary().FirstOrDefault(a => a.Key.Equals("id")).Value.ToString() : (this.callConfigWithClass) ? this.classElement : base.name) + "').TouchSpin({ " +
                                                          (min != null ? "min:" + min + "," : null) +
                                                          (max != null ? "max: " + max + "," : null) +
                //   stepinterval: 50,                     
                                                          (this.postfix != null ? "postfix: '" + postfix + "'," : null) +
                                                          (this.prefix != null ? "prefix: '" + prefix + "'," : null) +
                                                          (this.verticalbutton ? "verticalbuttons: true," : null) +
                                                      "});" +
                                                      "})" +
                                                      "</script>"
                                                      );

        }
    }
}
