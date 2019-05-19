using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class ListViewTourScheduleViewModel : BaseViewModelId
    {
        public ListViewTourScheduleViewModel()
        {
            CompanyTransfers = new List<ListViewTourScheduleCompanyTransferViewModel>();
        }
        /// <summary>
        /// شروع بازه زمانی تور
        /// </summary>
        public string FromDate { get; set; }
        /// <summary>
        /// پایان بازه زمانی تور 
        /// </summary>
        public string ToDate { get; set; }
        /// <summary>
        /// ظرفیت تور
        /// </summary>
        public int Capacity { get; set; }
        /// <summary>
        /// ظرفیت نامحدود تور ؟
        /// </summary>
        public bool NonLimit { get; set; }
        /// <summary>
        /// قمیت تور
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// تاریخ انقضای  تور که مشخص میکند تور تا چه تاریخی باز است
        /// </summary>
        public string ExpireDate { get; set; }
        /// <summary>
        /// آیدی تور
        /// </summary>
        public int TourId { get; set; }
        /// <summary>
        /// واحد پولی
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// آیدی تگ دیو هر برنامه سفر اضافه شده در ویو 
        /// </summary>
        public string SectionId { get; set; }
        public string TourTitle { get; set; }
        public string TourPackageTitle { get; set; }
        public int TourPackageId { get; set; }

        public List<ListViewTourScheduleCompanyTransferViewModel> CompanyTransfers { get; set; }
    }
}
