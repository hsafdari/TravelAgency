using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class HotelRoomPassengerSectionViewModel
    {
        /// <summary>
        /// دارای بزرگسال؟
        /// </summary>
        public bool HasAdult { get; set; }

        /// <summary>
        /// دارای کودک؟
        /// </summary>
        public bool HasChild { get; set; }

        /// <summary>
        /// دارای نوزاد؟
        /// </summary>
        public bool HasInfant { get; set; }

        /// <summary>
        /// تعداد مسافر برای این نوع اتاق
        /// </summary>
        public int PassengerCount { get; set; }
    }
}
