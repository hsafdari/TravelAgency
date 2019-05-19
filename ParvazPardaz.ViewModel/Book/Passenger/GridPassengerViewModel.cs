using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.ViewModel
{
    public class GridPassengerViewModel : BaseViewModelOfEntity
    {
        public GridPassengerViewModel()
        {
            this.SearchViewModel = new PassengerSearchParamViewModel();
        }
        public PassengerSearchParamViewModel SearchViewModel { get; set; }
        [Display(Name = "FirstName", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string LastName { get; set; }

        [Display(Name = "NationalCode", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string NationalCode { get; set; }
        [Display(Name = "Nationality", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string Nationality
        {
            get { return NationalityTitle.GetDisplayValue(); }                            
        }

        [Display(Name = "Gender", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string GenderTitle { get { return Gender.GetDisplayValue(); } }
        public Gender Gender { get; set; }
        [Display(Name = "AgeRange", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string AgeRangeTitle { get { return AgeRange.GetDisplayValue(); } }
        [Display(Name = "AgeRange", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public AgeRange AgeRange { get; set; }

        [Display(Name = "Currency", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string CurrencyTitle { get; set; }

        [Display(Name = "Order", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public long OrderId { get; set; }
         [Display(Name = "EnFirstName", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string EnFirstName { get; set; }
         [Display(Name = "EnLastName", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string EnLastName { get; set; }
         [Display(Name = "PassportNo", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string PassportNo { get; set; }
         [Display(Name = "PassportExpirationDate", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public Nullable<DateTime> PassportExpirationDate { get; set; }
        public EnumNationality NationalityTitle { get; set; }
           [Display(Name = "BirthCountry", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string BirthCountry { get; set; }
        [Display(Name = "PassportExporterCountry", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string PassportExporterCountry { get; set; }
        [Display(Name = "FlightDateTime", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public DateTime FlightDateTime { get; set; }
        [Display(Name = "ReturnFlightDateTime", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public DateTime? ReturnFlightDateTime { get; set; }
        [Display(Name = "Buyer", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string BuyerTitle { get; set; }
        [Display(Name = "TrackingCode", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string TrackingCode { get; set; }
        [Display(Name = "Birthdate", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public Nullable<DateTime> Birthdate { get; set; }
    }
}
