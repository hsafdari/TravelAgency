using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class TourScheduleHotelRoomViewModel 
    {
        public List<TourScheduleHotelRoomDynamicControl> TourScheduleHotelRoomDynamicControls { get; set; }
        public int TourScheduleId { get; set; }
        //public int HotelRoomId { get; set; }
        //public decimal? Price { get; set; }
        /// <summary>
        /// نشان میدهد در چه مدی است؟
        /// </summary>
        public CRUDMode CRUDMode { get; set; }
    }

    public class TourScheduleHotelRoomDynamicControl  :BaseViewModelId
    {
        public int TourScheduleId { get; set; }
        public int  HotelRoomId { get; set; }
        public string HotelRoomTitle { get; set; }
       
        public decimal? Price { get; set; }
        public bool  IsPrimary { get; set; }
        /// <summary>
        /// ظرفیت 
        /// </summary>
        [Display(Name = "Capacity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int? Capacity { get; set; }
        /// <summary>
        /// ظرفیت نامحدود ؟
        /// </summary>
        [Display(Name = "NonLimit", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public bool NonLimit { get; set; }
        /// <summary>
        /// نشان میدهد در چه مدی است؟
        /// </summary>
        public CRUDMode CRUDMode { get; set; }
    }
}
