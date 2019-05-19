using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class ListViewTourProgramViewModel : BaseViewModelId
    {
        /// <summary>
        /// مدت زمان برنامه سفر که تعداد روز را مشخص می کند
        /// </summary>
        [Display(Name = "DurationDay", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int DurationDay { get; set; }
        /// <summary>
        /// ترتیب برنامه سفر را در یک تور مشخص میکند
        /// </summary>
        [Display(Name = "DayOrder", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int DayOrder { get; set; }
        /// <summary>
        /// نام شهر
        /// </summary>
        [Display(Name = "CityTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string CityTitle { get; set; }
        /// <summary>
        /// فعالیت های برنامه سفر
        /// </summary>
        [Display(Name = "Activity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Activities { get; set; }
        /// <summary>
        /// توضیح برنامه سفر
        /// </summary>
        [Display(Name = "Description", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Description { get; set; }
        /// <summary>
        /// آیدی تگ دیو هر بخش اضافه شده 
        /// </summary>
        public string SectionId { get; set; }
        /// <summary>
        /// آیدی تور  
        /// </summary>
        public int TourId { get; set; }
    }
}
