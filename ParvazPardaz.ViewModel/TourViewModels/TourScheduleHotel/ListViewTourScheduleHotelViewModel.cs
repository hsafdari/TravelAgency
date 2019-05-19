using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class ListViewTourScheduleHotelViewModel : BaseViewModelId
    {
        /// <summary>
        /// عنوان هتل
        /// </summary>
        public string HotelTitle { get; set; }
        /// <summary>
        /// آیدی تگ دیو هر بخش اضافه شده 
        /// </summary>
        public string SectionId { get; set; }
        /// <summary>
        /// شناسه زمانبندی تور  
        /// </summary>
        public int TourScheduleId { get; set; }
        /// <summary>
        /// شناسه تور
        /// </summary>
        public int TourId { get; set; }
    }
}
