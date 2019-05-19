using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel.Book.LocationTour
{
    public class TourPackageLocationViewModel : BaseViewModelId
    {
        /// <summary>
        /// عنوان پکیج
        /// </summary>
        public string Title { get; set; }
        public string TourType { get; set; }
        public string TransferTypeTitle { get; set; }
        /// <summary>
        /// شرکت وسلیه حمل ونقل
        /// </summary>
        public string CompanyTransferTitle { get; set; }
        public string CompnayTransferLogo { get; set; }
        /// <summary>
        /// روز سفر مثال شش روز و هفت شب
        /// </summary>
        public string PackgeDayTitle { get; set; }
        /// <summary>
        /// تاریخ رفت و برگشت پرواز
        /// </summary>
        public string TransferDate { get; set; }
        /// <summary>
        /// مبلغ پایه فروش از اطلاعات را از مبلغ پکیج می خواند
        /// </summary>
        public string PackageStartPrice { get; set; }
        public string OfferPrice { get; set; }
        /// <summary>
        /// آدرس پکیج تور جهت هدایت صفحه 
        /// </summary>
        public string PackageUrl { get; set; }
        public string CompanyTransferIata { get; set; }
        public int CompanyTransferId { get; set; }
        public int? PackageDayId { get; set; }
        public int Priority { get; set; }
        public string ImageURL { get; set; }
        public DateTime FromDate { get; set; }
    }
}
