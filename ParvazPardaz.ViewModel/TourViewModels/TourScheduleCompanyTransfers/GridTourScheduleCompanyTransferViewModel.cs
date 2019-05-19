using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridTourScheduleCompanyTransferViewModel:BaseViewModelOfEntity
    {
         
       [Display(Name = "StartDateTime", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        /// <summary>
        /// تاریخ و زمان شروع حرکت
        /// </summary>
        /// 
        public DateTime StartDateTime { get; set; }
        /// <summary>
        /// تاریخ و زمان خاتمه حرکت
        /// </summary>
        /// 
       
       [Display(Name = "EndDateTime", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public DateTime EndDateTime { get; set; }
        /// <summary>
        /// مبدا حرکت
        /// </summary>
        /// 

       //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
       //[Display(Name = "ArrivalTime", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
       //public TimeSpan ArrivalTime { get; set; }

       //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
       //[Display(Name = "DepartureTime", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
       //public TimeSpan DepartureTime { get; set; }
       [Display(Name = "FromCity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int FromCityId { get; set; }
       public string FromCityTitle { get; set; }

        /// <summary>
        /// مقصد حرکت
        /// </summary>
        /// 
       [Display(Name = "DestinationCity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int DestinationCityId { get; set; }
       public string DestinationCityTitle { get; set; }
        /// <summary>
        /// مدت زمان مسیر
        /// </summary>
        /// 
       
       [Display(Name = "DurationTime", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
      
        public Nullable<TimeSpan> DurationTime { get; set; }
       
        /// <summary>
        /// ظرفیت وسلیه نقلیه
        /// </summary>
        /// 
       [Display(Name = "Capacity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int Capacity { get; set; }
        /// <summary>
        /// ظرفیت نامحدود ؟
        /// </summary>
        /// 
       [Display(Name = "NonLimit", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public bool NonLimit { get; set; }
        /// <summary>
        /// تعداد فروخته شده
        /// </summary>
        /// 

       public int TourScheduleId { get; set; }
       [ScaffoldColumn(false)]
        public int SoldQuantity { get; set; }

       public int VehicleTypeId { get; set; }

       public int CompanyTransferId { get; set; }
       public string CompanyTransfer { get; set; }
    }
    
}
