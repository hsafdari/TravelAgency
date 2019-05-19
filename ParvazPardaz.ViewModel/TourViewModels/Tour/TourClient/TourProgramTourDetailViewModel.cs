using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class TourProgramTourDetailViewModel
    {

        /// <summary>
        /// مدت زمان برنامه سفر که تعداد روز را مشخص می کند
        /// </summary>
        public int DurationDay { get; set; }
        /// <summary>
        /// ترتیب برنامه سفر را در یک تور مشخص میکند
        /// </summary>
        public int DayOrder { get; set; }
        /// <summary>
        /// شهر محل اقامت این برنامه سفر را مشخص میکند
        /// </summary>
        public int CityId { get; set; }
        public string CityTitle { get; set; }
        /// <summary>
        /// توضیح
        /// </summary>
        public string Description { get; set; }

        public int TourId { get; set; }
        
    } 
}
