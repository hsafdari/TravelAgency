using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class ListViewTourScheduleAdditionalServiceViewModel : BaseViewModelId
    {
        /// <summary>
        /// قیمت خدمات 
        /// </summary>
        [Display(Name = "Price", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [DataType("decimal(5,2)")]
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
        /// تعداد فروخته شده
        /// </summary>
        [Display(Name = "SoldQuantity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int SoldQuantity { get; set; }
        /// <summary>
        /// شناسه زمانبندی تور
        /// </summary>
        [Display(Name = "TourSchedule", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int TourScheduleId { get; set; }
        /// <summary>
        /// عنوان خدمات اضافه تور
        /// </summary>
        [Display(Name = "AdditionalService", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string AdditionalServiceTitle { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        [Display(Name = "Description", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Description { get; set; }
        /// <summary>
        /// آیدی تگ دیو هر برنامه سفر اضافه شده در ویو 
        /// </summary>
        public string SectionId { get; set; }
    }
}
