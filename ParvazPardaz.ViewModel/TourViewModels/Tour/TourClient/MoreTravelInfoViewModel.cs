using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
   public class MoreTravelInfoViewModel:BaseViewModelId
    {
        public int TourID { get; set; }
        public int schID { get; set; }
        public int firstSchId { get; set; }
        public GridMoreTravelInfoViewModel GridMoreTravelInfoViewModel { get; set; }
        ///// <summary>
        ///// تاریخ و زمان شروع حرکت
        ///// </summary>
        //public DateTime StartDateTime { get; set; }
        ///// <summary>
        ///// تاریخ و زمان خاتمه حرکت
        ///// </summary>
        //public DateTime EndDateTime { get; set; }
        ///// <summary>
        ///// مبدا حرکت
        ///// </summary>
        //public int FromCityId { get; set; }
        //public string FromCityTitle { get; set; }
        ///// <summary>
        ///// مقصد حرکت
        ///// </summary>
        //public int DestinationCityId { get; set; }
        //public string DestinationCityTitle { get; set; }
        ///// <summary>
        ///// مدت رمان مسیر
        ///// </summary>
        //public DateTime? DurationTime { get; set; }
        ///// <summary>
        ///// ظرفیت وسلیه نقلیه
        ///// </summary>
        //public int Capacity { get; set; }
        ///// <summary>
        ///// ظرفیت نامحدود ؟
        ///// </summary>
        //public bool NonLimit { get; set; }
        ///// <summary>
        ///// تعداد فروخته شده
        ///// </summary>
        //public int SoldQuantity { get; set; }
    }


   public class GridMoreTravelInfoViewModel:BaseViewModelId
   {
       /// <summary>
       /// تاریخ و زمان شروع حرکت
       /// </summary>
       public string StartDateTime { get; set; }
       /// <summary>
       /// تاریخ و زمان خاتمه حرکت
       /// </summary>
       public string EndDateTime { get; set; }
       /// <summary>
       /// مبدا حرکت
       /// </summary>
       public int FromCityId { get; set; }
       public string FromCityTitle { get; set; }
       /// <summary>
       /// مقصد حرکت
       /// </summary>
       public int DestinationCityId { get; set; }
       public string DestinationCityTitle { get; set; }
       /// <summary>
       /// مدت رمان مسیر
       /// </summary>
       public DateTime? DurationTime { get; set; }
       /// <summary>
       /// ظرفیت وسلیه نقلیه
       /// </summary>
       public int Capacity { get; set; }
       /// <summary>
       /// ظرفیت نامحدود ؟
       /// </summary>
       public bool NonLimit { get; set; }
       /// <summary>
       /// تعداد فروخته شده
       /// </summary>
       public int SoldQuantity { get; set; }
       public string CompanyTransfer { get; set; }
       public int scheduleID { get; set; }
       public int TourID { get; set; }
      
       
   }
   public class TourScheduleUIViewModel : BaseViewModelId
   {
       public DateTime startTime { get; set; }
   }
}
