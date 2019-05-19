using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ParvazPardaz.Common.Extension;
using System.Text.RegularExpressions;

namespace ParvazPardaz.Common.HtmlHelpers
{
    public static class RateHelper
    {
        public static Rate Rate(this HtmlHelper htmlHelper, string name, string propSetValue = null)
        {
            return new Rate(htmlHelper, name, propSetValue);
        }

        public static RateFor<TModel, TProperty> RateFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, string name, Expression<Func<TModel, TProperty>> expression)
        {
            return new RateFor<TModel, TProperty>(htmlHelper, name, expression);
        }
    }

    public class Rate : BaseComponent
    {
        public Rate(HtmlHelper htmlHelper, string name, string propSetValue)
        {
            base.name = name;
            base.htmlHelper = htmlHelper;
            numberStar = 5;
            isRtl = false;
            isReadonly = false;
            this.propSetValue = propSetValue;
            this.postActionURL = string.Empty;
            this.postActionPropId = 0;
            this.isCollectionItem = false;
            this.starWidth = string.Empty;
        }

        public enum Star
        {
            HalfStar = 1, FullStar = 2
        }

        #region Properties
        private int numberStar { get; set; }
        private decimal rate { get; set; }
        private int precision { get; set; }
        private bool isRtl { get; set; }
        private bool isReadonly { get; set; }
        private Star? starType { get; set; }
        private string starWidth { get; set; }
        private string propSetValue { get; set; }
        private bool isCollectionItem { get; set; }
        private string collectionItemId { get; set; }
        /// <summary>
        /// آدرس اکشنی که می خواهیم امتیاز را به آن بفرستیم
        /// </summary>
        private string postActionURL { get; set; }
        /// <summary>
        /// شناسه ی موجودیتی که می خواهیم امتیاز برای وی ثبت گردد
        /// </summary>
        private long postActionPropId { get; set; }


        #endregion

        #region Methods

        public Rate NumberStar(int numberStar)
        {
            this.numberStar = numberStar;
            return this;
        }


        public Rate Rating(decimal rate)
        {
            this.rate = rate;
            return this;
        }

        public Rate Precision(int precision)
        {
            this.precision = precision;
            return this;
        }

        public Rate IsRtl(bool isRtl)
        {
            this.isRtl = isRtl;
            return this;
        }

        public Rate IsReadonly(bool isReadonly)
        {
            this.isReadonly = isReadonly;
            return this;
        }

        public Rate StarType(Star starType)
        {
            this.starType = starType;
            return this;
        }

        public Rate StarWidth(string starWidth)
        {
            this.starWidth = starWidth;
            return this;
        }

        public Rate PostRateToURLAjax(string postActionURL, int postActionPropId)
        {
            this.postActionURL = postActionURL;
            this.postActionPropId = postActionPropId;
            return this;
        }

        public Rate PostRateToURLAjax(string postActionURL, long postActionPropId)
        {
            this.postActionURL = postActionURL;
            this.postActionPropId = postActionPropId;
            return this;
        }

        public Rate IsCollectionItem(bool isCollectionItem = false)
        {
            this.isCollectionItem = isCollectionItem;
            this.collectionItemId = this.propSetValue.Replace('[', '_').Replace(']', '_').Replace('.', '_');
            return this;
        }
        //public Rate PropSetValue(string propSetValue)
        //{
        //    this.propSetValue = propSetValue;
        //    return this;
        //}
        //public Rate PropSetValue<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
        //{
        //    this.propSetValue = propSetValue;
        //    return this;
        //}
        #endregion


        public override string ToString()
        {
            ConfigScript();
            var str = "<div id=\"" + base.name + "\"></div>" + (propSetValue != null ? System.Web.Mvc.Html.InputExtensions.Hidden(base.htmlHelper, propSetValue, null).ToHtmlString() : null);
            //System.Web.Mvc.Html.InputExtensions.TextBox(base.htmlHelper, propSetValue, null).ToHtmlString();
            return str;
        }

        private void ConfigScript()
        {
            //var R = jQuery.noConflict();
            base.htmlHelper.ViewContext.Writer.Write("<script>  $(document).ready(function () { " +
                                "$(\"#" + base.name + "\").rateYo({" +
                                        "ratedFill: \"#00A694\"," +
                                        "rating:" + rate + "," +
                                        "precision:" + precision + "," +
                                        "rtl: " + isRtl.ToString().ToLower() + "," +
                                        "readOnly: " + isReadonly.ToString().ToLower() + "," +
                                        "numStars: " + numberStar + "," +
                                        (this.starWidth != string.Empty ? "starWidth: \"" + this.starWidth + "\"," : null) +
                                        (this.starType != null && this.starType == Star.FullStar ? "fullStar: true," : this.starType != null && this.starType == Star.HalfStar ? "halfStar: true," : null) +
                                        (this.propSetValue != null ? "onSet: function (rating, rateYoInstance) {" +
                                        "$('#" + (this.isCollectionItem ? this.collectionItemId : this.propSetValue) + "').val(rating);" +
                                                (this.postActionURL != string.Empty ?
                                                    "$.ajax({" +
                                                        "type: \"POST\"," +
                                                        "url: \"" + this.postActionURL + "\"," +
                                                        "data: {" +
                                                            "Id: " + this.postActionPropId + "," +
                                                            "Rate: rating" +
                                                       " }," +
                                                        "success: function (data) {" +
                                                            "if(data.Report == \"Success\")" +
                                                            "{" +
                                                                "$('#" + base.name + " svg').on(\"click\",function(e){ e.preventDefault();});" +
                                                                "toastr.options = { \"positionClass\": \"toast-top-center\" };" +
                                                                "toastr.success( data.Message , data.Title , { timeOut: 5000 });" +
                                                            "}" +
                                                            "else if(data.Report == \"Multiple\")" +
                                                            "{" +
                                                                "toastr.options = { \"positionClass\": \"toast-top-center\" };" +
                                                                "toastr.error( data.Message , data.Title , { timeOut: 5000 });" +
                                                            "}" +
                                                            "else if(data.Report == \"Error\")" +
                                                            "{" +
                                                                "toastr.options = { \"positionClass\": \"toast-top-center\" };" +
                                                                "toastr.error( data.Message , data.Title , { timeOut: 5000 });" +
                                                            "}" +
                                                       " }," +
                                                        "error: function (textStatus, errorThrown) {" +
                                                            "toastr.options = { \"positionClass\": \"toast-top-center\" };" +
                                                            "toastr.error( data.Message , data.Title , { timeOut: 5000 });" +
                                                        "}" +
                                                    "}); " : null) +
                                            "}" : null) +
                                    "});" +
                                "})" +
                                "</script>");
        }
    }

    public class RateFor<TModel, TProperty> : Rate
    {
        public RateFor(HtmlHelper<TModel> htmlHelper, string name, Expression<Func<TModel, TProperty>> expression)
            : base(htmlHelper, name, expression.MemberWithoutInstance())
        {

        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
