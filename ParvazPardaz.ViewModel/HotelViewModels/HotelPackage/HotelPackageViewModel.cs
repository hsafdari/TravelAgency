using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class HotelPackageViewModel : BaseViewModelId
    {
        #region Constructor
        public HotelPackageViewModel()
        {
            HotelsInPackage = new List<HotelsInPackageViewModel>();
           // HotelRoomsInPackage = new List<HotelRoomsInPackageViewModel>();
        }
        #endregion

        #region Properties
        public int index { get; set; }
        public int TourPackageId { get; set; }
        [Display(Name = "OrderHotelPackage", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int OrderId { get; set; }
        public List<HotelsInPackageViewModel> HotelsInPackage { get; set; }
        public List<HotelRoomsInPackageViewModel> HotelRoomsInPackage { get; set; }

        /// <summary>
        /// شناسه تور
        /// </summary>
        public int TourId { get; set; }
        public CRUDMode CRUDMode { get; set; }
        public string SectionId { get; set; }

        public List<ListViewHotelPackageViewModel> ListView { get; set; }
        #endregion

        #region افزودن زمانبندی سفر، از پکیج توری دیگر
        public int? SelectedTourId { get; set; }
        public int? SelectedTourPackageId { get; set; }
        #endregion
    }

    public class HotelsInPackageViewModel : BaseViewModelId
    {
        public int TourPackageId { get; set; }

        [Display(Name = "HotelTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int HotelId { get; set; }

        [Display(Name = "HotelTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string HotelTitle { get; set; }

        [Display(Name = "CityTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int CityId { get; set; }

        [Display(Name = "CityTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string CityTitle { get; set; }

        public string Location { get; set; }
        public string RankLogo { get; set; }
        public string Summary { get; set; }

        [Display(Name = "HotelBoard", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public Nullable<int> HotelBoardId { get; set; }
        public string HotelBoardTitle { get; set; }
        public string HotelBoardLogo { get; set; }

        /// <summary>
        /// تصویر بندانگشتی
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// لیستی از تصاویر هتل
        /// </summary>
        public List<string> HotelGalleryImages { get; set; }

        public int RankOrderId { get; set; }
    }

    public class HotelRoomsInPackageViewModel : BaseViewModelId
    {
        #region Constructor
        public HotelRoomsInPackageViewModel()
        {
            //Capacity = 0;
            AdultCapacity = 0;
            ChildCapacity = 0;
            InfantCapacity = 0;
            AdultPrice = 0;
            ChildPrice = 0;
            InfantPrice = 0;
        }
        #endregion

        public string Title { get; set; }
        public int RoomTypeId { get; set; }

        ///// <summary>
        ///// قیمت
        ///// </summary>
        //[Display(Name = "Price", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        //[DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //public decimal Price { get; set; }

        /// <summary>
        /// قیمت
        /// </summary>
        [Display(Name = "AdultPrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public decimal AdultPrice { get; set; }

        /// <summary>
        /// قیمت
        /// </summary>
        [Display(Name = "ChildPrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public decimal ChildPrice { get; set; }

        /// <summary>
        /// قیمت
        /// </summary>
        [Display(Name = "InfantPrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public decimal InfantPrice { get; set; }


        ///// <summary>
        ///// موجودی 
        ///// </summary>
        //[Display(Name = "Capacity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //public int Capacity { get; set; }

        /// <summary>
        /// موجودی 
        /// </summary>
        [Display(Name = "AdultCapacity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int AdultCapacity { get; set; }


        /// <summary>
        /// موجودی 
        /// </summary>
        [Display(Name = "ChildCapacity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int ChildCapacity { get; set; }

        /// <summary>
        /// موجودی 
        /// </summary>
        [Display(Name = "InfantCapacity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int InfantCapacity { get; set; }


        ///// <summary>
        ///// ظرفیت فروخته شده
        ///// </summary>
        //public int CapacitySold { get; set; }

        /// <summary>
        /// ظرفیت فروخته شده بزرگسال
        /// </summary>
        public int AdultCapacitySold { get; set; }

        /// <summary>
        /// ظرفیت فروخته شده کودک
        /// </summary>
        public int ChildCapacitySold { get; set; }

        /// <summary>
        /// ظرفیت فروخته شده نوزاد
        /// </summary>
        public int InfantCapacitySold { get; set; }

        //public Nullable<decimal> OtherCurrencyPrice { get; set; }
        public Nullable<decimal> AdultOtherCurrencyPrice { get; set; }
        public Nullable<decimal> ChildOtherCurrencyPrice { get; set; }
        public Nullable<decimal> InfantOtherCurrencyPrice { get; set; }

        public Nullable<int> OtherCurrencyId { get; set; }
        public bool IsPrimary { get; set; }
        public CRUDMode CRUDMode { get; set; }
    }
}
