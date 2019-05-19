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
    public class EditHotelViewModel:BaseViewModelId
    {
        /// <summary>
        /// نام هتل
        /// </summary>
        /// 
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [StringLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //[RegularExpression(@"^[^<>{}\[\],*?+\\/.؟]+$", ErrorMessage = "کاراکتر ممنوعه")]
        [RegularExpression(@"^[^<>{}\[\],*+\\/.]+$", ErrorMessage = "کاراکتر ممنوعه")]
        public string Title { get; set; }
        /// <summary>
        /// تلفن هتل
        /// </summary>
        /// 
        [Display(Name = "Tel", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [StringLength(15, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Tel { get; set; }
        /// <summary>
        /// آدرس وبسایت هتل
        /// </summary>
        /// 
        [Display(Name = "Website", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [StringLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Website { get; set; }
        ///// <summary>
        ///// کشور هتل
        ///// </summary>
        ///// 
        //[Display(Name = "Country", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        //public int CountryId { get; set; }
        //public IEnumerable<SelectListItem> CountryList { get; set; }
        /// <summary>
        /// شهر هتل
        /// </summary>
        [Display(Name = "CityTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int CityId { get; set; }

        [Display(Name = "CityTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string CityTitle { get; set; }

        /// <summary>
        /// هتل چند ستاره است؟
        /// </summary>
        [Display(Name = "HotelRank", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int RateId { get; set; }

        /// <summary>
        /// کل رتبه های حذف نشده
        /// </summary>
        public IEnumerable<SelectListItem> RateList { get; set; }

        /// <summary>
        /// موقعیت هتل
        /// </summary>
        [Display(Name = "HotelLocation", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Location { get; set; }

        /// <summary>
        /// آدرس هتل
        /// </summary>
        /// 
        [Display(Name = "Address", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [StringLength(150, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Address { get; set; }

        /// <summary>
        /// طول جغرافیایی
        /// </summary>
        [Display(Name = "Latitude", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Latitude { get; set; }
        /// <summary>
        /// عرض جغرافیایی
        /// </summary>
        [Display(Name = "Longitude", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Longitude { get; set; }
        /// <summary>
        /// نقشه گوگل : آی-فریم طول و عرض جغرافیایی
        /// </summary>
        [Display(Name = "LatLongIframe", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [UIHint("TinyMCE_Modern")]
        [AllowHtml]
        public string LatLongIframe { get; set; }

        /// <summary>
        /// توضیحات
        /// </summary>
        /// 
        [UIHint("TinyMCE_Modern")]
        [AllowHtml]
        [Display(Name = "Description", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Description { get; set; }

        [Display(Name = "HotelFacility", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public List<int> HotelFacility { get; set; }

        [Display(Name = "IsSummary", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public bool IsSummary { get; set; }
        [Display(Name = "HotelRule", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string HotelRule { get; set; }
        [Display(Name = "CancelationPolicy", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string CancelationPolicy { get; set; }

        #region from Post ViewModel

        [Display(Name = "ServiceDesc", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        //[Display(Name = "PostSummery", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string Summary { get; set; }

        [Display(Name = "MetaKeywords", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string MetaKeywords { get; set; }

        [Display(Name = "MetaDescription", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string MetaDescription { get; set; }

        //public int VisitCount { get; set; }
        //public decimal PostRateAvg { get; set; }
        //public int PostRateCount { get; set; }
        //public DateTime PublishDatetime { get; set; }

        [Display(Name = "PublishDatetime", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //[UIHint("KendoDateTimePicker")]
        public Nullable<DateTime> PublishDatetime { get; set; }

        //[Display(Name = "ExpireDatetime", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        ////[UIHint("KendoDateTimePicker")]
        //public Nullable<DateTime> ExpireDatetime { get; set; }

        [Display(Name = "SortOrder", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int Sort { get; set; }

        [Display(Name = "AccessLevel", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public AccessLevel AccessLevel { get; set; }

        [Display(Name = "IsActiveComments", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public bool IsActiveComments { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public bool IsActive { get; set; }

        [Display(Name = "Thumbnail", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public HttpPostedFileBase File { get; set; }

        /// <summary>
        /// مسیر عکسی  
        /// </summary>
        [ScaffoldColumn(false)]
        public string ImageUrl { get; set; }

        /// <summary>
        /// پسوند عکس
        /// </summary>
        [ScaffoldColumn(false)]
        public string ImageExtension { get; set; }

        /// <summary>
        /// نام فایل عکس
        /// </summary>
        [ScaffoldColumn(false)]
        public string ImageFileName { get; set; }

        /// <summary>
        /// اندازه فایل
        /// </summary>
        [ScaffoldColumn(false)]
        public long ImageSize { get; set; }

        #region Collection of Tags
        [Display(Name = "Keywords", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public List<string> TagTitles { get; set; }
        public IEnumerable<SelectListItem> KeywordsDDL { get; set; }
        #endregion

        #region SelectedGroups
        public virtual List<PostGroup> _postGroups { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "حداقل یک نقش را انتخاب نمایید")]
        public List<int> _selectedPostGroups { get; set; }
        #endregion


        #endregion
    }
}
