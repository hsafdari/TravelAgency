using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ParvazPardaz.Common.Utility;
using System.Linq.Expressions;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Common.HtmlHelpers.Models;
using GeneralResource = ParvazPardaz.Resource.General.Generals;

namespace ParvazPardaz.Common.HtmlHelpers
{
    public static class FileUploaderHelper
    {
        public static FileUploader FileUploader(this HtmlHelper htmlHelper, string name)
        {
            return new FileUploader(htmlHelper, name);
        }

        public static FileUploaderFor<TModel, TProperty> FileUploaderFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return new FileUploaderFor<TModel, TProperty>(htmlHelper, expression);
        }
    }
    public class FileUploader : BaseComponent
    {
        #region Ctor
        public FileUploader(HtmlHelper htmlHelper, string name)
        {
            base.name = name;
            base.htmlHelper = htmlHelper;
            limit = null;
            maxSize = null;
            extensions = new List<string>();
            uploadParameters = new Dictionary<string, object>();
            removeParameters = new Dictionary<string, object>();
            isRequired = false;
        }
        #endregion

        #region Properties
        private bool isMultiple { get; set; }
        private bool showThumbs { get; set; }
        private bool addMore { get; set; }
        private bool itemAppendToEnd { get; set; }
        private bool removeConfirmation { get; set; }
        private bool removeButton { get; set; }
        private bool removeButtonInEditMode { get; set; }
        private int? limit { get; set; }
        private int? maxSize { get; set; }
        private List<string> extensions { get; set; }
        private string edit { get; set; }
        private string uploadUrl { get; set; }
        private string removeUrl { get; set; }
        private Dictionary<string, object> uploadParameters { get; set; }
        private Dictionary<string, object> removeParameters { get; set; }
        private string buttonLbl { get; set; }
        private string feedbackLbl { get; set; }
        private string feedback2Lbl { get; set; }
        private string dropLbl { get; set; }
        private string removeConfirmationLbl { get; set; }
        private bool isRequired { get; set; }
        private string validationMessage { get; set; }
        #endregion

        #region  Edit Properties
        private string fileName { get; set; }
        private string fileSize { get; set; }
        private string fileType { get; set; }
        private string fileUrl { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// امکان الزام آور بودن آن را فراهم می کند
        /// </summary>
        /// <param name="isRequired">لازم است؟</param>
        /// <param name="validationMessage">پیام اعتبارسنجی</param>
        /// <returns>بارگذار فایل</returns>
        public FileUploader IsRequired(bool isRequired, string validationMessage)
        {
            this.isRequired = isRequired;
            this.validationMessage = validationMessage;
            return (FileUploader)this;
        }
        /// <summary>
        /// انتخاب بیش از یک فایل را امکانپذیر میکند 
        /// </summary>
        /// <param name="isMultiple"></param>
        /// <returns></returns>
        public FileUploader IsMultiple(bool isMultiple)
        {
            this.isMultiple = isMultiple;
            return (FileUploader)this;
        }
        /// <summary>
        ///  جهت نمایش لیست فایلهای انتخاب شده و نمایش عکس کوچکی از هر فایل
        /// </summary>
        /// <param name="showThumbs"></param>
        /// <returns></returns>
        public FileUploader ShowThumbs(bool showThumbs)
        {
            this.showThumbs = showThumbs;
            return (FileUploader)this;
        }
        /// <summary>
        /// افزودن فایل در انتهای فایلهای انتخاب شده را امکاپذیر میکند
        /// </summary>
        /// <param name="addMore"></param>
        /// <returns></returns>
        public FileUploader AddMore(bool addMore)
        {
            this.addMore = addMore;
            return (FileUploader)this;
        }
        /// <summary>
        /// افزودن فایلهای انتخاب شده در انتهای لیست فایلهای انتخاب شده وگرنه در ابتدای این لیست قرار میگیرد
        /// </summary>
        /// <param name="itemAppendToEnd"></param>
        /// <returns></returns>
        public FileUploader ItemAppendToEnd(bool itemAppendToEnd)
        {
            this.itemAppendToEnd = itemAppendToEnd;
            return (FileUploader)this;
        }
        /// <summary>
        /// تایددیه حذف فایلهای انتخاب شده
        /// </summary>
        /// <param name="removeConfirmation"></param>
        /// <returns></returns>
        public FileUploader RemoveConfirmation(bool removeConfirmation)
        {
            this.removeConfirmation = removeConfirmation;
            return (FileUploader)this;
        }
        /// <summary>
        /// فعال کردن دکمه حذف
        /// </summary>
        /// <param name="removeButton"></param>
        /// <returns></returns>
        public FileUploader RemoveButton(bool removeButton)
        {
            this.removeButton = removeButton;
            return (FileUploader)this;
        }
        /// <summary>
        /// دکمه حذف در حالت ویرایش
        /// </summary>
        /// <param name="removeButtonInEditMode"></param>
        /// <returns></returns>
        public FileUploader RemoveButtonInEditMode(bool removeButtonInEditMode)
        {
            this.removeButtonInEditMode = removeButtonInEditMode;
            return (FileUploader)this;
        }
        /// <summary>
        /// محدویت تعداد فایلها
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public FileUploader Limit(int limit)
        {
            this.limit = limit;
            return (FileUploader)this;
        }
        /// <summary>
        /// حداکثر اندازه فایلها
        /// </summary>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        public FileUploader MaxSize(int maxSize)
        {
            this.maxSize = maxSize;
            return (FileUploader)this;
        }
        /// <summary>
        /// نوع فایلهای مجاز
        /// </summary>
        /// <param name="extensions"></param>
        /// <returns></returns>
        public FileUploader Extension(List<string> extensions)
        {
            this.extensions = extensions;
            return (FileUploader)this;
        }
        /// <summary>
        /// اکشن و کنترولر آپلود و پارامترهای ورودی اکشن
        /// </summary>
        /// <param name="uploadUrl"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public FileUploader UploadUrl(string uploadUrl, Dictionary<string, object> parameters)
        {
            this.uploadUrl = uploadUrl;
            this.uploadParameters = parameters;
            return (FileUploader)this;
        }
        /// <summary>
        /// جایگذاری متن کپشن های آپلودر
        /// </summary>
        /// <param name="button"></param>
        /// <param name="feedback"></param>
        /// <param name="feedback2"></param>
        /// <param name="drop"></param>
        /// <param name="removeConfirmation"></param>
        /// <returns></returns>
        public FileUploader Captions(string button, string feedback = null, string feedback2 = null, string drop = null, string removeConfirmation = null)
        {
            this.buttonLbl = button;
            this.feedbackLbl = feedback;
            this.feedback2Lbl = feedback2;
            this.dropLbl = drop;
            this.removeConfirmationLbl = removeConfirmation;
            return (FileUploader)this;
        }
        /// <summary>
        /// اکشن و کنترولر حذف و پارامترهای ورودی اکشن
        /// </summary>
        /// <param name="uploadUrl"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public FileUploader RemoveUrl(string uploadUrl, Dictionary<string, object> parameters = null)
        {
            this.removeUrl = uploadUrl;
            this.removeParameters = parameters;
            return (FileUploader)this;
        }
        /// <summary>
        /// مقدار دهی با یک کلاس شامل عضوهای نام و اندازه و نوع و مسیر فایل برای ویرایش فایلها 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="edit"></param>
        /// <param name="fileNameMember"></param>
        /// <param name="fileSizeMember"></param>
        /// <param name="fileTypeMemeber"></param>
        /// <param name="fileUrlMember"></param>
        /// <returns></returns>
        public FileUploader Edit<TModel>(IEnumerable<TModel> edit, Expression<Func<TModel, string>> fileNameMember, Expression<Func<TModel, long>> fileSizeMember, Expression<Func<TModel, string>> fileTypeMemeber, Expression<Func<TModel, string>> fileUrlMember)
        {
            this.fileName = fileNameMember.MemberWithoutInstance();
            this.fileSize = fileSizeMember.MemberWithoutInstance();
            this.fileType = fileTypeMemeber.MemberWithoutInstance();
            this.fileUrl = fileUrlMember.MemberWithoutInstance();
            var str = " files: [ ";
            foreach (var item in edit.ToList())
            {
                str += "{" +
                         "name:" + item.GetPropValue(this.fileName) + "," +
                         "size:" + item.GetPropValue(this.fileSize) + "," +
                         "type:" + item.GetPropValue(this.fileType) + "," +
                         "file:" + item.GetPropValue(this.fileUrl) + "," +
                       "},";
            }
            str += "]";
            this.edit = str;
            return (FileUploader)this;
        }


        public FileUploader Edit(List<EditModeFileUpload> edit)
        {
            var str = " files: [ ";
            if (edit != null && edit.Any())
            {
                foreach (var item in edit)
                {
                    str += "{" +
                             "name:" + "'" + item.Name + "'" + "," +
                             "size:" + item.Size + "," +
                             "type:" + "'" + item.Type + "'" + "," +
                             "file:" + "'" + item.Url + "'" + "," +
                             "id:" + item.Id +
                           "},";
                }
            }

            str += "]";
            this.edit = str;
            return (FileUploader)this;
        }


        public override string ToString()
        {
            string str = "<input type=\"file\" name=\"" + base.name + "\"  id=\"" + base.name + "\"" + (this.isMultiple ? " multiple=\"multiple\"" : null) + (this.isRequired ? " required=\"\" title=\"" + this.validationMessage + "\" " : null) + ">";
            ScriptManeger();
            return str;
        }


        private void ScriptManeger()
        {
            htmlHelper.ViewContext.Writer.Write("<script type=\"text/javascript\"> " +
                 "jQuery(document).ready(function(){ " +
                         " jQuery('#" + base.name + "').filer({ " +
                         "showThumbs : " + (showThumbs ? "true" : "false") + "," +
                         "addMore: " + (this.addMore ? "true" : "false") + "," +
                         "limit:" + (this.limit != null ? limit.Value.ToString() : "null") + "," +
                         "maxSize : " + (this.maxSize != null ? maxSize.Value.ToString() : "null") + "," +
                         "extensions :" + (this.extensions.Any() ? "[" + String.Join(",", this.extensions.Select(s => "'" + s + "'").ToList()) + "]" : "null") + "," +
                         (this.showThumbs ? "templates: {" +
                                               "box: '<ul class=\"jFiler-items-list jFiler-items-grid\"></ul>', " +
                                               "item: '<li class=\"jFiler-item\">\\" +
                                                  "<div class=\"jFiler-item-container\">\\" +
                                                       "<div class=\"jFiler-item-inner\">\\" +
                                                          "<div class=\"jFiler-item-thumb\">\\" +
                                                               "<div class=\"jFiler-item-status\"></div>\\" +
                                                               "<div class=\"jFiler-item-info\">\\" +
                                                                   "<span class=\"jFiler-item-title\"><b title=\"{{fi-name}}\">{{fi-name | limitTo: 25}}</b></span>\\" +
                                                                   "<span class=\"jFiler-item-others\">{{fi-size2}}</span>\\" +
                                                               "</div>\\" +
                                                               "{{fi-image}}\\" +
                                                           "</div>\\" +
                                                           "<div class=\"jFiler-item-assets jFiler-row\">\\" +
                                                               "<ul class=\"list-inline pull-left\">\\" +
                                                                   "<li>{{fi-progressBar}}</li>\\" +
                                                               "</ul>\\" +
                                                               "<ul class=\"list-inline pull-right\">\\" +
                                                               (this.removeButton ? "<li><a class=\"icon-jfi-trash jFiler-item-trash-action\"></a></li>\\" : null) +
                                                               "</ul>\\" +
                                                          "</div>\\" +
                                                       "</div>\\" +
                                                   "</div>\\" +
                                                  "</li>'," +
                                            "itemAppend: '<li class=\"jFiler-item\">\\" +
                                                    "<div class=\"jFiler-item-container\">\\" +
                                                        "<div class=\"jFiler-item-inner\">\\" +
                                                            "<div class=\"jFiler-item-thumb\">\\" +
                                                                "<div class=\"jFiler-item-status\"></div>\\" +
                                                                "<div class=\"jFiler-item-info\">\\" +
                                                                    "<span class=\"jFiler-item-title\"><b title=\"{{fi-name}}\">{{fi-name | limitTo: 25}}</b></span>\\" +
                                                                    "<span class=\"jFiler-item-others\">{{fi-size2}}</span>\\" +
                                                                "</div>\\" +
                                                                "{{fi-image}}\\" +
                                                            "</div>\\" +
                                                            "<div class=\"jFiler-item-assets jFiler-row\">\\" +
                                                                "<ul class=\"list-inline pull-left\">\\" +
                                                                    "<li><span class=\"jFiler-item-others\">{{fi-icon}}</span></li>\\" +
                                                                "</ul>\\" +
                                                                "<ul class=\"list-inline pull-right\">\\" +
                                                               (this.removeButtonInEditMode ? "<li><a class=\"icon-jfi-trash jFiler-item-trash-action\"></a></li>\\" : null) +
                                                                "</ul>\\" +
                                                            "</div>\\" +
                                                        "</div>\\" +
                                                    "</div>\\" +
                                                "</li>'," +
                //progressBar: '<div class="bar"></div>',
                "itemAppendToEnd:" + (this.itemAppendToEnd && this.addMore ? "true" : "false") + "," +
                "removeConfirmation:" + (this.removeConfirmation ? "true" : "false") + "," +
                "_selectors: {" +
                    "list: '.jFiler-items-list'," +
                    "item: '.jFiler-item'," +
                    "progressBar: '.bar'," +
                    (this.removeButton || this.removeButtonInEditMode ? "remove: '.jFiler-item-trash-action'" : null) +
                "}" +
            "}," : null) +
                (this.uploadUrl != null ? "uploadFile: { " +
                               "url: \"" + this.uploadUrl + "\"" + "," +
                               "data: " + (this.uploadParameters.Any() ? "{" + uploadParameters.CreateStringParams() + "}" : "null") + "," +
                               "type: " + "'POST'" + "," +
                               "enctype: " + "'multipart/form-data' " + "," +
                               "beforeSend:" + "function () { }," +
                               "success: function (data, el) { " +
                                   "var parent = el.find(\".jFiler-jProgressBar\").parent();" +
                                   "el.find(\".jFiler-jProgressBar\").fadeOut(\"slow\", function () {" +
                                       "jQuery('<div class=\"jFiler-item-others text-success\"><i class=\"icon-jfi-check-circle\"></i> Success</div>').hide().appendTo(parent).fadeIn(\"slow\");" +
                                   "});" +
                               "}," +
                               "error: function (el) {" +
                                   "var parent = el.find(\".jFiler-jProgressBar\").parent();" +
                                   "el.find(\".jFiler-jProgressBar\").fadeOut(\"slow\", function () {" +
                                       "jQuery('<div class=\"jFiler-item-others text-error\"><i class=\"icon-jfi-minus-circle\"></i> Error</div>').hide().appendTo(parent).fadeIn(\"slow\");" +
                                   "});" +
                               "}," +
                               "statusCode: null," +
                               "onProgress: null," +
                               "onComplete: null" +
                               "}," : null) +
                " captions: {" +
                "button:'" + (this.buttonLbl != null ? this.buttonLbl : GeneralResource.captionsButton) + "'," +
                "feedback:'" + (this.feedbackLbl != null ? this.feedbackLbl : GeneralResource.captionsFeedback) + "'," +
                "feedback2:'" + (this.feedback2Lbl != null ? this.feedback2Lbl : GeneralResource.captionsFeedback2) + "'," +
                "drop:'" + (this.dropLbl != null ? this.dropLbl : GeneralResource.captionsDrop) + "'," +
                "removeConfirmation:'" + (this.removeConfirmationLbl != null ? this.removeConfirmationLbl : GeneralResource.captionsRemoveConfirmation) + "'," +
                    "errors: {" +
                        "filesLimit:'" + GeneralResource.captionsErFilesLimit + "'," +
                        "filesType:'" + GeneralResource.captionsErFilesType + "'," +
                        "filesSize:'" + GeneralResource.captionsErFilesSize + "'," +
                        "filesSizeAll:'" + GeneralResource.captionsErFilesSizeAll + "'," +
                        "folderUpload:'" + GeneralResource.captionsErFolderUpload + "'" +
                    "}" +
                "}," +
                (this.edit != null ? this.edit + "," : null) +
                (this.removeUrl != null ? "onRemove :" + GetAjaxScript(removeUrl, removeParameters) : null) +

                //close filer
            "});" +
                //close decument ready 
        "})" +
        "</script>");
        }
        #endregion

        private string GetAjaxScript(string url, Dictionary<string, object> parameters = null)
        {
            return "function (e, data) {" +

                    "   jQuery.ajax({" +
                    "      url:  '" + url + "'" + "," +
                    "      method:   'POST'," +
                    "      cache: false  , " +
                    "      dataType: 'json'," +
                    "      data :  " + "{" + (parameters != null ? parameters.CreateStringParams() : null) + "id : data.id" + "}" + "," +
                    "      success:  function(data) {" +

                    "      }" +
                    "   });" +
                    "" +
                    "}";
        }

    }

    public class FileUploaderFor<TModel, TProperty> : FileUploader
    {
        public FileUploaderFor(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
            : base(htmlHelper, expression.MemberWithoutInstance())
        {
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
