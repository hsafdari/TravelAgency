using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class ListViewTourPackageViewModel : BaseViewModelId
    {
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Title { get; set; }

        [Display(Name = "Description", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Description { get; set; }

        [Display(Name = "DateTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string DateTitle { get; set; }

        [Display(Name = "Code", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Code { get; set; }

        [Display(Name = "FromPrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string FromPrice { get; set; }
        [Display(Name = "OfferPrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string OfferPrice { get; set; }
        [Display(Name = "OwnerId", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public Nullable<int> OwnerId { get; set; }
        public string  OwnerTitle { get; set; }
        public int TourId { get; set; }
        public string SectionId { get; set; }
        public string TourTitle { get; set; }
        [Display(Name = "Priority", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public int Priority { get; set; }        
        public string CityTitle { get; set; }
        public string CountryTitle { get; set; }
        [Display(Name = "TourPackgeDayId", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TourPackageDaysTitle { get; set; }

    }
}
