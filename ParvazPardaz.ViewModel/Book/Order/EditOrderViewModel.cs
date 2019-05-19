using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class EditOrderViewModel : BaseViewModelBigId
    {
        #region Properties
        /// <summary>
        /// کد پیگیری
        /// </summary>
        [Display(Name = "TrackingCode", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string TrackingCode { get; set; }

        /// <summary>
        /// قیمت کل
        /// </summary>
        [Display(Name = "TotalPrice", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// تخفیف کل
        /// </summary>
        [Display(Name = "TotalDiscountPrice", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public decimal TotalDiscountPrice { get; set; }

        /// <summary>
        /// مالیات کل
        /// </summary>
        [Display(Name = "TotalTaxPrice", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public decimal TotalTaxPrice { get; set; }

        /// <summary>
        /// کل مبلغ قابل پرداخت
        /// </summary>
        [Display(Name = "TotalPayPrice", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public decimal TotalPayPrice { get; set; }

        /// <summary>
        /// واحد پولی
        /// </summary>
        [Display(Name = "Currency", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int CurrencyId { get; set; }

        /// <summary>
        /// قیمت پایه دلار
        /// </summary>
        [Display(Name = "DollarBasePrice", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public decimal DollarBasePrice { get; set; }

        /// <summary>
        /// وضعیت سفارش
        /// </summary>
        [Display(Name = "OrderStep", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public EnumOrderStep OrderStep { get; set; }

        /// <summary>
        /// تاریخ-زمان پرواز
        /// </summary>
        [Display(Name = "FlightDateTime", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public DateTime FlightDateTime { get; set; }

        /// <summary>
        /// شماره رفرنس بلیط پرواز
        /// </summary>
        [Display(Name = "TicketReferenceNo", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string TicketReferenceNo { get; set; }

        /// <summary>
        /// تعداد بزرگسال
        /// </summary>
        [Display(Name = "AdultCount", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int AdultCount { get; set; }

        /// <summary>
        /// تعداد کودک
        /// </summary>
        [Display(Name = "ChildCount", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int ChildCount { get; set; }

        /// <summary>
        /// تعداد نوزاد
        /// </summary>
        [Display(Name = "InfantCount", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int InfantCount { get; set; }

        /// <summary>
        /// ظرفیت باقیمانده پرواز
        /// </summary>
        [Display(Name = "RemainingFlightCapacity", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int RemainingFlightCapacity { get; set; }

        /// <summary>
        /// موفق؟
        /// </summary>
        [Display(Name = "IsSuccessful", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public bool IsSuccessful { get; set; } 
        #endregion
    }
}
