using ParvazPardaz.Model.Entity.Post;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class AddTourViewModel : BaseViewModelId
    {
        #region Constructor
        public AddTourViewModel()
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// عنوان تور
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //[Remote("CheckURLInLinkTable", "Tour")]
        public string Title { get; set; }

        #region DDL : Country, City, TourLandingPageUrls
        /// <summary>
        /// شناسه کشور
        /// </summary>
        [Display(Name = "Country", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public Nullable<int> CountryId { get; set; }
        /// <summary>
        /// کل کشورهای حذف نـشده
        /// </summary>
        public IEnumerable<SelectListItem> CountryDDL { get; set; }
        /// <summary>
        /// شناسه شهر
        /// </summary>
        [Display(Name = "City", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public Nullable<int> CityId { get; set; }
        /// <summary>
        /// کل شهرهای حذف نـشده
        /// </summary>
        public IEnumerable<SelectListItem> CityDDL { get; set; }
        ///// <summary>
        ///// شناسه آدرس لندینگ پیج تور
        ///// </summary>
        //[Display(Name = "URL", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        ////[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //public Nullable<int> TourLandingPageUrlId { get; set; }
        ///// <summary>
        ///// کل آدرس های لندینگ پیج حذف نـشده
        ///// </summary>
        //public IEnumerable<SelectListItem> TourLandingPageUrlDDL { get; set; }
        #endregion

        /// <summary>
        /// آدرس انگلیسی تور با الگوی زیر ، مورد استفاده در جدول لینک ها
        /// Country-City-TourTitle
        /// </summary>
        [Display(Name = "LinkTableTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //[Remote("CheckURLInLinkTable", "Tour")]
        public string LinkTableTitle { get; set; }

        /// <summary>
        /// نشان میدهد که تور از سمت آژانس توصیه میشود ؟
        /// </summary>
        [Display(Name = "Recomended", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        //[Remote("IsUrlSelected", "Tour", "Admin", AdditionalFields = "TourLandingPageUrlId", ErrorMessageResourceName = "UrlRequired", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public bool Recomended { get; set; }

        /// <summary>
        /// توضیحات تور
        /// </summary>
        //[UIHint("TinyMCE_Modern")]
        [AllowHtml]
        [Display(Name = "Description", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Description { get; set; }

        /// <summary>
        /// کد تور  به صورت کدی منحصر به فرد در تور تعریف گردد این کد به صورت حروف و اعداد می باشد که با کد عددی زمابندی جمع می شود
        /// شاید این کد منحصر به فرد را در سیستم حسابداری خود وارد نمایند که درآمدشان را محاسبه کند
        /// </summary>
        [Display(Name = "TourCode", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Remote("IsUniqueTourCode", "Tour")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Code { get; set; }

        /// <summary>
        /// لیست آیدیهای انتخاب شده برای جدول نوع تور
        /// </summary>
        [Display(Name = "TourType", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public List<int> SelectedTourType { get; set; }
        
        /// <summary>
        /// لیست آیدی های انتخاب شده برای سطح تور
        /// </summary>
        [Display(Name = "TourLevel", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public List<int> SelectedTourLevel { get; set; }
        /// <summary>
        /// لیست آیدی های انتخاب شده برای جدول دسته بندی تور
        /// </summary>
        [Display(Name = "TourCategory", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public List<int> SelectedTourCategory { get; set; }
        /// <summary>
        /// لیست آیدی های انتخاب شده که بایستی به صورت مجاز ثبت شوند
        /// </summary>
        [Display(Name = "Allows", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public List<int> SelectedAllows { get; set; }
        /// <summary>
        /// لیست آیدی های انتخاب شده که بایستی به صورت غیر مجاز ثبت شوند
        /// </summary>
        [Display(Name = "Bans", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public List<int> SelectedBans { get; set; }
        public CRUDMode CRUDMode { get; set; }

        /// <summary>
        /// برای نمایش در صفحه اول
        /// </summary>
        [Display(Name = "ShortDescription", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string ShortDescription { get; set; }
        #endregion

        #region SelectedGroups
        public virtual List<PostGroup> _postGroups { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "حداقل یک گروه را انتخاب نمایید")]
        public List<int> _selectedPostGroups { get; set; }
        #endregion

        #region Required documents
        [Display(Name = "RequiredDocuments", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public List<int> RequiredDocumentIds { get; set; }
        #endregion
    }
}
