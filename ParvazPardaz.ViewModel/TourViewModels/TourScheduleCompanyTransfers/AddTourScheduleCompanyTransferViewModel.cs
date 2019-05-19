using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
   public class AddTourScheduleCompanyTransferViewModel:BaseViewModelId
    {
       
       [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
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
       [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
       [Display(Name = "EndDateTime", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public DateTime EndDateTime { get; set; }
        /// <summary>
        /// مبدا حرکت
        /// </summary>
        /// 

       [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
       [Display(Name = "ArrivalTime", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
       public Nullable<TimeSpan> ArrivalTime { get; set; }

       [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
       [Display(Name = "DepartureTime", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
       public Nullable<TimeSpan> DepartureTime { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
       [Display(Name = "FromCity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int FromCityId { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
       public string FromCityTitle { get; set; }

        /// <summary>
        /// مقصد حرکت
        /// </summary>
        /// 
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
       [Display(Name = "DestinationCity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int DestinationCityId { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
       public string DestinationCityTitle { get; set; }
        /// <summary>
        /// مدت زمان مسیر
        /// </summary>
        /// 
       
       [Display(Name = "DurationTime", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
      
        public DateTime? DurationTime { get; set; }
       
        /// <summary>
        /// ظرفیت وسلیه نقلیه
        /// </summary>
        /// 
       [Display(Name = "Capacity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
       [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
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

       [Display(Name = "VehicleType", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
       public int VehicleTypeId { get; set; }
       public IEnumerable<SelectListItem> VehicleTypeList { get; set; }

        
       [Display(Name = "CompanyTransfer", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
       [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
       public Nullable<int> CompanyTransferId { get; set; }
       public IEnumerable<SelectListItem> CompanyTransferList { get; set; }
    }
}
