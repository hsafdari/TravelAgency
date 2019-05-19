using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class TourScheduleAdditionalServiceViewModel : BaseViewModelId
    {
        /// <summary>
        /// قیمت خدمات 
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "Price", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "#,##0.00")]
        //[DisplayFormat(DataFormatString = "{0:N2}")]
        //[RegularExpression("^(?!0?(,0?0)?$)([0-9]{0,3}(,[0-9]{1,2})?)?$", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public decimal Price { get; set; }
        /// <summary>
        /// ظرفیت خدمات
        /// </summary>
        [Display(Name = "Capacity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int Capacity { get; set; }
        /// <summary>
        /// ظرفیت نامحدود خدمات ؟
        /// </summary>
        [Display(Name = "NonLimit", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public bool NonLimit { get; set; }
        /// <summary>
        /// شناسه زمانبندی تور
        /// </summary>
        [Display(Name = "TourSchedule", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int TourScheduleId { get; set; }
        /// <summary>
        /// شناسه خدمات اضافه تور
        /// </summary>
        [Display(Name = "AdditionalService", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int AdditionalServiceId { get; set; }
        /// <summary>
        /// سلکت-لیست-آیتم هایی از خدمات اضافی تور
        /// </summary>
        public IEnumerable<SelectListItem> AdditionalServiceDDL { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        [Display(Name = "Description", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Description { get; set; }        
        /// <summary>
        /// نشان میدهد در چه مدی است؟
        /// </summary>
        public CRUDMode CRUDMode { get; set; }
        /// <summary>
        /// آیدی تگ دیو هر برنامه سفر اضافه شده در ویو 
        /// </summary>
        public string SectionId { get; set; }
        /// <summary>
        /// لیستی از خدمات اضافی افزوده شده جهت نمایش در صفحه در حالت رفرش ضفحه
        /// </summary>
        public List<ListViewTourScheduleAdditionalServiceViewModel> ListView { get; set; }
    }
}
