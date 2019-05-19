using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class TourProgramViewModel : BaseViewModelId
    {
        /// <summary>
        /// مدت زمان برنامه سفر که تعداد روز را مشخص می کند
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "Duration", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int DurationDay { get; set; }
        /// <summary>
        /// ترتیب برنامه سفر را در یک تور مشخص میکند
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "Day", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int DayOrder { get; set; }
        /// <summary>
        /// شهر محل اقامت این برنامه سفر را مشخص میکند
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// نام شهر
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "CityTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string CityTitle { get; set; }
        /// <summary>
        /// عنوان روز ، قبلا : توضیحات برنامه سفر 
        /// </summary>
        [Display(Name = "DayTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        //[StringLength(500, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Description { get; set; }
        /// <summary>
        /// لیست آیدیهای فعالیت های انتخاب شده
        /// </summary>
        [Display(Name = "Activity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public List<int> SelectedActivities { get; set; }
        /// <summary>
        /// آیدی تور  
        /// </summary>
        public int TourId { get; set; }
        /// <summary>
        /// نشان میدهد در چه مدی است؟
        /// </summary>
        public CRUDMode CRUDMode { get; set; }
        /// <summary>
        /// آیدی تگ دیو هر بخش اضافه شده 
        /// </summary>
        public string SectionId { get; set; }
        /// <summary>
        /// لیستی از بخش های افزوده شده جهت نمایش در صفحه در حالت رفرش صفحه
        /// </summary>
        public List<ListViewTourProgramViewModel> ListView { get; set; }

        #region افزودن برنامه سفر از توری دیگر
        public int? SelectedTourId { get; set; }
        #endregion
    }

 
}
