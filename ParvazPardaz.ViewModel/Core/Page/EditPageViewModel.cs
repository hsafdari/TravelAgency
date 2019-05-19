using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;

namespace ParvazPardaz.ViewModel
{
    public class EditPageViewModel : BaseViewModelId
    {
        [MaxLength(300)]
        [Display(Name = "PageTitle", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string PageTitle { get; set; }

        [MaxLength(300)]
        [Display(Name = "PageName", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string PageName { get; set; }

        [ScaffoldColumn(false)]
        public string PageHeaderImg { get; set; }

        [Display(Name = "PageHeaderImg", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public HttpPostedFileBase File { get; set; }

        [MaxLength(400)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "PageDesc", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string PageDesc { get; set; }

        [UIHint("TinyMCE_Modern")]
        [Display(Name = "PageContent", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string PageContent { get; set; }

        [MaxLength(300)]
        [Display(Name = "PageMetaTags", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string pageMetatages { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public bool PageIsActive { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "PageDatetime", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public Nullable<System.DateTime> PageDatetime { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "PageUserId", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public Nullable<int> PageUserId { get; set; }

        [Display(Name = "IsCommentActive", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public bool IsActiveComments { get; set; }

    }
}
