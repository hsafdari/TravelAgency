using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Enum;
using System.Web.Mvc;
namespace ParvazPardaz.ViewModel
{
    public class AddPassengerViewModel : BaseViewModelId
    {
        #region En Properties
        [Display(Name = "EnFirstName", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string EnFirstName { get; set; }

        [Display(Name = "EnLastName", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string EnLastName { get; set; }

        [Display(Name = "PassportNo", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string PassportNo { get; set; }

        [Display(Name = "BirthCountry", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public Nullable<int> BirthCountryId { get; set; }
        public string BirthCountryTitle { get; set; }
        public string EnBirthCountryTitle { get; set; }

        [Display(Name = "PassportExporterCountry", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public Nullable<int> PassportExporterCountryId { get; set; }

        [Display(Name = "PassportExpirationDate", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //[Remote("IsPassportExpirationDateValid", "Tour", null, ErrorMessageResourceName = "PassportExpirationDateValidate", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public Nullable<DateTime> PassportExpirationDate { get; set; }

        [Display(Name = "Nationality", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public EnumNationality Nationality { get; set; }
        #endregion

        #region Fa Properties
        [Display(Name = "FirstName", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string LastName { get; set; }

        [Display(Name = "Birthdate", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public Nullable<DateTime> Birthdate { get; set; }

        [Display(Name = "NationalCode", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "{0} را به درستی وارد نمایید")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string NationalCode { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public Gender Gender { get; set; }

        [Display(Name = "AgeRange", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public AgeRange AgeRange { get; set; }
        #endregion

        #region RoomSection Property
        /// <summary>
        /// ایندکس اتاق ؛ که مشخص شه این مسافر برای کدوم اتاق در نظر گرفته شده بوده
        /// </summary>
        public int RoomIndex { get; set; }

        /// <summary>
        /// قیمت این مسافر در اتاقی که قرار داره چقدره؟
        /// قیمت = هم قیمت ریالی و هم ارزی در نظر گرفته شه
        /// </summary>
        public Nullable<Decimal> AgeRangePriceInThisRoom { get; set; }
        #endregion
    }
}