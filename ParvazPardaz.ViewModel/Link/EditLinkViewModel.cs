using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class EditLinkViewModel : BaseViewModelBigId
    {
        public int typeId { get; set; }

        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string URL { get; set; }
        public string ControllerUrl
        {
            get
            {
                string ctrl = this.URL.Split('/')[1];
                return ctrl == null ? string.Empty : ctrl;
                //this.URL.Substring(this.URL.LastIndexOf("/") + 1, (this.URL.Length) - (this.URL.LastIndexOf("/") + 1));
            }
            set
            {
                //value = this.URL.Substring(0,2); // this.URL.Substring(this.URL.LastIndexOf("/") + 1, (this.URL.Length) - (this.URL.LastIndexOf("/") + 1));
            }
        }
        public string PrefixUrl
        {
            get
            {
                var orgUrl = this.URL;

                var controllerUrl = this.URL.Split('/')[1];

                string urlnoSlash = this.URL.Substring(0, this.URL.Length - 1);
                int startInx = urlnoSlash.LastIndexOf("/") + 1;
                int lng = urlnoSlash.Length - (urlnoSlash.LastIndexOf("/") + 1);
                var endUrl = urlnoSlash.Substring(startInx, lng).ToString();
                string pre;
                if ((controllerUrl.Length + 2 + endUrl.Length + 1) >= orgUrl.Length)
                {
                    pre = null;
                }
                else
                {
                    pre = orgUrl.Substring(controllerUrl.Length + 2, orgUrl.Length - (endUrl.Length + 2) - (controllerUrl.Length + 2));
                }

                return pre;
            }
            set { }
        }
        public string EndUrl
        {
            get
            {
                string urlnoSlash = this.URL.Substring(0, this.URL.Length - 1);

                int startInx = urlnoSlash.LastIndexOf("/") + 1;
                int lng = urlnoSlash.Length - (urlnoSlash.LastIndexOf("/") + 1);
                return urlnoSlash.Substring(startInx, lng).ToString();
            }
            set { }

        }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Target { get; set; }
        public IEnumerable<SelectListItem> TargetDDL { get; set; }
        public string Rel { get; set; }
        public IEnumerable<SelectListItem> RelDDL { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public bool Visible { get; set; }
        public LinkType linkType { get; set; }

        [AllowHtml]
        public string CustomMetaTags { get; set; }

        public Nullable<EnumChangeFreq> Changefreq { get; set; }

        [RegularExpression(@"^\s*([0-1]|[0].\d{1,2})\s*$", ErrorMessageResourceName = "SiteMapPriorityValidation", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public Nullable<decimal> Priority { get; set; }
    }
}
