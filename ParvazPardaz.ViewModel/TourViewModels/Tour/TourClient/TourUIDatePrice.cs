using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class TourUIDatePrice
    {
        public IEnumerable<TourSchViewModel> TourSchedule { get; set; }
        public IEnumerable<TourUIAdditionalCostsViewModel> TourAdditionalCost { get; set; }
        public IEnumerable<AdditionalDescription> AdditionalService { get; set; }
    }
    public class TourSchViewModel : BaseViewModelId
    {
        /// <summary>
        /// شروع بازه زمانی تور
        /// </summary>
        public DateTime FromDate { get; set; }
        /// <summary>
        /// پایان بازه زمانی تور 
        /// </summary>
        public DateTime ToDate { get; set; }
        /// <summary>
        /// قمیت تور
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// ظرفیت تور
        /// </summary>
        public int Capacity { get; set; }
        /// <summary>
        /// ظرفیت نامحدود تور ؟
        /// </summary>
        public bool NonLimit { get; set; }
        /// <summary>
        /// تاریخ انقضای  تور که مشخص میکند تور تا چه تاریخی باز است
        /// </summary>
        public DateTime ExpireDate { get; set; }
        /// <summary>
        /// تعداد فروخته شده
        /// </summary>
        public int SoldQuantity { get; set; }
        public int CurrencyId { get; set; }
        public string Currency { get; set; }
        public int TourId { get; set; }
        public string TourTitle { get; set; }
        //public IEnumerable<additionalCost> additionalCost { get; set; }
    }
    public class TourUIAdditionalCostsViewModel : BaseViewModelId
    {
        public string Title { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public bool IsDelete { get; set; }
        public string Currency { get; set; }
        
    }
     public class AdditionalDescription
    {
        public string Title { get; set; }
        public string AdditionalServiceDescription { get; set; }
        public IEnumerable<string> scheduleAdditionalServiceDescription { get; set; }
        public bool IsDelete { get; set; }
    }
}
