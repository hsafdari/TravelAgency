using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class TourScheduleHotelViewModel : BaseViewModelId
    {
        #region Add Hotels
        /// <summary>
        /// شناسه هتل
        /// </summary>
        public int HotelId { get; set; }
        /// <summary>
        /// عنوان هتل
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "Hotel", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string HotelTitle { get; set; }
        /// <summary>
        /// شناسه زمانبندی تور
        /// </summary>
        public int TourScheduleId { get; set; }
        /// <summary>
        /// شناسه تور
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
        public List<ListViewTourScheduleHotelViewModel> ListView { get; set; }
        #endregion       
    }
}
