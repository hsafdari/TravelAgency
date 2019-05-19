using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Tour;

namespace ParvazPardaz.ViewModel
{
    public class DayToDayViewModel
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
        public virtual IEnumerable<TourProgramDetailUIViewModel> TourProgramDetails { get; set; }
       
    }


    public class TourProgramDetailUIViewModel : BaseViewModelId
    {
        /// <summary>
        /// عنوان برنامه سفر
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// توضیح
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// مسیر عکسی از شهر
        /// </summary>
        public string ImageUrl { get; set; }

        public int TourProgramId { get; set; }
    }
}
