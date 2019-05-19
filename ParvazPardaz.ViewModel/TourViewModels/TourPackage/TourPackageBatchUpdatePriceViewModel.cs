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
    public class TourPackageBatchUpdatePriceViewModel
    {
        #region Constructor
        public TourPackageBatchUpdatePriceViewModel()
        {

        }
        #endregion

        #region DropDownList Properties
        /// <summary>
        /// تور
        /// </summary>
        [Display(Name = "Tour", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int TourId { get; set; }

        /// <summary>
        /// پکیج تور
        /// </summary>
        [Display(Name = "TourPackage", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int TourPackageId { get; set; }
        #endregion

        #region Tour Package Update Properties
        /// <summary>
        /// شروع قیمت پکیج تور
        /// </summary>
        [Display(Name = "TourPacageFromPrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TourPacageFromPrice { get; set; }

        /// <summary>
        /// نوع افزایش/کاهش قیمت
        /// </summary>
        [Display(Name = "EnumUpdatePriceType", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public EnumUpdatePriceType EnumUpdatePriceType { get; set; }

        /// <summary>
        /// میزان افزایش/کاهش قیمت انواع تخت در پکیج هتل - واحد پولی ایران
        /// </summary>
        [Display(Name = "IranUpdatePriceVal", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public decimal IranUpdatePriceVal { get; set; }

        /// <summary>
        /// قیمت ارزی ویرایش شود؟
        /// </summary>
        [Display(Name = "IsUpdateCurrencyPrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public bool IsUpdateCurrencyPrice { get; set; }

        /// <summary>
        /// میزان افزایش/کاهش قیمت انواع تخت در پکیج هتل - واحدهای پولی ارزی
        /// </summary>
        [Display(Name = "CurrencyUpdatePriceVal", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public Nullable<decimal> CurrencyUpdatePriceVal { get; set; }
        [Display(Name = "OfferPrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string OfferPrice { get; set; }
        #endregion
    }
}
