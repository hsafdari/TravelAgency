using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class ReportGridViewModel : BaseViewModelOfEntity
    {
        /// <summary>
        /// نام تور
        /// </summary>
        [Display(Name = "TourTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TourTitle { get; set; }
        /// <summary>
        /// قیمت تور
        /// </summary>
        [Display(Name = "Price", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public decimal Price { get; set; }
        /// <summary>
        /// تاریخ آغاز تور
        /// </summary>
        [Display(Name = "FromDate", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public DateTime StartDate { get; set; }
        /// <summary>
        /// تاریخ خاتمه تور
        /// </summary>
        [Display(Name = "ToDate", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public DateTime EndDate { get; set; }
        /// <summary>
        /// شناسه رزرو آنلاین تور
        /// </summary>
        public string ReferenceId { get; set; }
        /// <summary>
        /// تعداد فروخته شده
        /// </summary>
        public int SoldQuantity { get; set; }
        /// <summary>
        /// تعداد مسافر رزرو آنلاین تور
        /// </summary>
        public int PassengerQuantity { get; set; }
        /// <summary>
        /// ظرفیت تور
        /// </summary>
        public int TourCapacity { get; set; }
        /// <summary>
        /// کد تور
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// قیمت کل - قیمت تور در تعداد مسافر
        /// </summary>
        public decimal  TotalPrice { get; set; }
    }
}
