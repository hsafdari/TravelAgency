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
    public class EditContentViewModel : BaseViewModelId
    {
        #region Constructor
        public EditContentViewModel()
        {

        }
        #endregion

        #region properties
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Title { get; set; }

        //[Display(Name = "NavigationUrl", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //[Remote("IsUniqueUrlInEdit", "Content", AdditionalFields = "Id", ErrorMessageResourceName = "Duplicate", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string NavigationUrl { get; set; }

        //[ScaffoldColumn(false)]
        //public string _NavigationUrl { get { return "/tour/" + this.NavigationUrl + "-offer/"; } }

        #region Image properties
        [Display(Name = "File", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public HttpPostedFileBase File { get; set; }
        [ScaffoldColumn(false)]
        public string ImageUrl { get; set; }
        [ScaffoldColumn(false)]
        public string ImageExtension { get; set; }
        [ScaffoldColumn(false)]
        public string ImageFileName { get; set; }
        [ScaffoldColumn(false)]
        public long ImageSize { get; set; }
        #endregion

        [Display(Name = "Description", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Description { get; set; }

        [Display(Name = "Context", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [UIHint("TinyMCE_Modern")]
        [AllowHtml]
        public string Context { get; set; }

        [Display(Name = "ContentDateTime", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public Nullable<DateTime> ContentDateTime { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Remote("IsUrlSelected", "Content", "Admin", AdditionalFields = "TourLandingPageUrlId", ErrorMessageResourceName = "UrlRequired", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public bool IsActive { get; set; }

        [Display(Name = "CommentIsActive", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public bool CommentIsActive { get; set; }

        #region DDL : Country, City, TourLandingPageUrls
        /// <summary>
        /// شناسه کشور
        /// </summary>
        [Display(Name = "Country", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public Nullable<int> CountryId { get; set; }
        /// <summary>
        /// کل کشورهای حذف نـشده
        /// </summary>
        public IEnumerable<SelectListItem> CountryDDL { get; set; }
        /// <summary>
        /// شناسه شهر
        /// </summary>
        [Display(Name = "City", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public Nullable<int> CityId { get; set; }
        /// <summary>
        /// کل شهرهای حذف نـشده
        /// </summary>
        public IEnumerable<SelectListItem> CityDDL { get; set; }
        /// <summary>
        /// شناسه آدرس لندینگ پیج تور
        /// </summary>
        [Display(Name = "URL", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public Nullable<int> TourLandingPageUrlId { get; set; }
        /// <summary>
        /// کل آدرس های لندینگ پیج حذف نـشده
        /// </summary>
        public IEnumerable<SelectListItem> TourLandingPageUrlDDL { get; set; }
        #endregion

        #endregion

        #region Reference navigation properties
        [Display(Name = "ContentGroup", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int ContentGroupId { get; set; }
        public SelectList ContentGroupDDL { get; set; }
        #endregion
    }
}
