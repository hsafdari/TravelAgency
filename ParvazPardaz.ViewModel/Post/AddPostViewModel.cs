using ParvazPardaz.Model.Entity.Post;
using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class AddPostViewModel : BaseViewModelId
    {


        public virtual List<PostGroup> _postGroups { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "حداقل یک نقش را انتخاب نمایید")]
        public List<int> _selectedPostGroups { get; set; }


        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //[Remote("CheckURL", "Post")]
        [RegularExpression(@"^[^<>{}\[\],*?+\\/.؟]+$", ErrorMessage = "کاراکتر ممنوعه")]
        public string Name { get; set; }

        [Display(Name = "LinkTableTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Remote("CheckLinkTableURL", "Post")]
        [RegularExpression(@"^[^<>{}\[\],*?+\\/.؟]+$", ErrorMessage = "کاراکتر ممنوعه")]
        public string LinkTableTitle { get; set; }

        [Display(Name = "Thumbnail", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public HttpPostedFileBase File { get; set; }

        [Display(Name = "PostSummery", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string PostSummery { get; set; }

        [Display(Name = "PostContent", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [UIHint("TinyMCE_Modern")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string PostContent { get; set; }

        [Display(Name = "MetaKeywords", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string MetaKeywords { get; set; }

        [Display(Name = "MetaDescription", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string MetaDescription { get; set; }

        // [Display(Name = "VisitCount", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //public int VisitCount { get; set; }

        //[Display(Name = "PostRateAvg", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //public decimal PostRateAvg { get; set; }

        //[Display(Name = "PostRateCount", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //public int PostRateCount { get; set; }

        [Display(Name = "PublishDatetime", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //[UIHint("KendoDateTimePicker")]
        public Nullable<DateTime> PublishDatetime { get; set; }

        ////[Display(Name = "PublishDatetime", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        ////[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        ////[UIHint("KendoDateTimePicker")]
        ////public string _PublishDatetime
        ////{
        ////    get
        ////    {
        ////        return string.Empty;
        ////    }
        ////    set
        ////    {
        ////        if (value != null)
        ////            PublishDatetime = Convert.ToDateTime(value);

        ////    }
        ////}

        [Display(Name = "ExpireDatetime", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //[UIHint("KendoDateTimePicker")]
        public Nullable<DateTime> ExpireDatetime { get; set; }
        //[Display(Name = "ExpireDatetime", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //[UIHint("KendoDateTimePicker")]
        //public string _ExpireDatetime
        //{
        //    get
        //    {
        //        return string.Empty;
        //    }
        //    set
        //    {
        //        if (value != null)
        //            ExpireDatetime = Convert.ToDateTime(value);

        //    }
        //}

        [Display(Name = "SortOrder", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int PostSort { get; set; }

        [Display(Name = "AccessLevel", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public AccessLevel AccessLevel { get; set; }

        [Display(Name = "IsActiveComments", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public bool IsActiveComments { get; set; }
        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public bool IsActive { get; set; }

        #region Collection of Tags
        [Display(Name = "Keywords", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public List<string> TagTitles { get; set; }
        public IEnumerable<SelectListItem> KeywordsDDL { get; set; }
        #endregion
    }
}
