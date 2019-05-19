using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class TourScheduleViewModel : BaseViewModelId
    {
        #region Properties
        /// <summary>
        /// شروع بازه زمانی تور
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "FromDate", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [DataType(DataType.DateTime)]
        public DateTime FromDate { get; set; }
        /// <summary>
        /// پایان بازه زمانی تور 
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "ToDate", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [DataType(DataType.DateTime)]
        public DateTime ToDate { get; set; }
        /// <summary>
        /// ظرفیت تور
        /// </summary>
        [Display(Name = "Capacity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int Capacity { get; set; }
        /// <summary>
        /// ظرفیت نامحدود تور ؟
        /// </summary>
        [Display(Name = "NonLimit", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public bool NonLimit { get; set; }
        /// <summary>
        /// قمیت تور
        /// </summary>
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "Price", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public decimal? Price { get; set; }
        /// <summary>
        /// تاریخ انقضای  تور که مشخص میکند تور تا چه تاریخی باز است
        /// </summary>
        [Display(Name = "ExpireDate", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [DataType(DataType.DateTime)]
        public DateTime ExpireDate { get; set; }
        /// <summary>
        /// آیدی تور
        /// </summary>
        public int TourId { get; set; }
        public int TourPackageId { get; set; }
        /// <summary>
        /// واحد پولی
        /// </summary>
        [Display(Name = "Currency", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int? CurrencyId { get; set; }
        /// <summary>
        /// نشان میدهد در چه مدی است؟
        /// </summary>
        public CRUDMode CRUDMode { get; set; }
        /// <summary>
        /// آیدی تگ دیو هر زمانبندی تور اضافه شده در ویو 
        /// </summary>
        public string SectionId { get; set; } 
        #endregion

        #region افزودن زمانبندی سفر، از پکیج توری دیگر
        public int? SelectedTourId { get; set; }
        public int? SelectedTourPackageId { get; set; }
        #endregion

        #region Collection navigation properties
        /// <summary>
        /// لیستی از زمانبندی های تور افزوده شده جهت نمایش در صفحه در حالت رفرش ضفحه
        /// </summary>
        public List<ListViewTourScheduleViewModel> ListView { get; set; }
        public List<TourScheduleCompanyTransferViewModel> CompanyTransfer { get; set; } 
        #endregion

        #region for patialviews
        public int index { get; set; }
        #endregion
    }
}
