using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class BookViewModel : BaseViewModelId
    {
        public int TourScheduleId { get; set; }
        public string HeaderImageURL { get; set; }

        public string TourTitle { get; set; }

        #region Add Passengers For Section 1
        /// <summary>
        /// لیستی از مسافران  
        /// </summary>
        public List<PassengerDynamicControl> PassengerDynamicControls { get; set; }
        /// <summary>
        /// شمارنده
        /// </summary>
        public int Counter { get; set; }
        /// <summary>
        /// آیدی تگ دیو هر برنامه سفر اضافه شده در ویو 
        /// </summary>
        public string SectionId { get; set; }
        #endregion

        #region  Extra For Section 2
        /// <summary>
        /// اگر درست باشد لیست اتاق های هتل انتخاب شده است
        /// </summary>
        public bool RdbCheckHotelRoom { get; set; }
        /// <summary>
        /// لیست خدمات اضافه 
        /// </summary>
        public List<AdditionalServiceControl> AdditionalServiceControls { get; set; }
        /// <summary>
        /// لیست اتاق های هتل 
        /// </summary>
        public List<HotelRoomDynamicControl> HotelRoomDynamicControls { get; set; }
        #endregion

        #region Price Summary For Section 3
        public List<PriceSummary> PriceSummaries { get; set; }
        #endregion

        #region Add Passengers Details For Section 4
        public List<PassengerDetailControl> PassengerDetailControls { get; set; }
        #endregion
    }
}
