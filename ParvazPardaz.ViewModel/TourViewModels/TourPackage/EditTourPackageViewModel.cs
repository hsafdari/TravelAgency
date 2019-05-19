using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class EditTourPackageViewModel: BaseViewModelId
    {
        #region Properties.
        [Display(Name = "TourTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TourTitle { get; set; }
        [Display(Name = "TourCode", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public string TourCode { get; set; }
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Title { get; set; }
        [Display(Name = "DateTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string DateTitle { get; set; }
        [Display(Name = "Code", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Code { get; set; }
        public Nullable<int> OwnerId { get; set; }
        [Display(Name = "FromPrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string FromPrice { get; set; }
        [Display(Name = "OfferPrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string OfferPrice { get; set; }
        [Display(Name = "Priority", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int Priority { get; set; }
        [Display(Name = "DayTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string DayTitle { get; set; }
        [Display(Name = "Description", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Description { get; set; }
        [Display(Name = "ImageURL", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string ImageURL { get; set; }
        [Display(Name = "ImageURL", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public HttpPostedFileBase File { get; set; }
        [Display(Name = "DayTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public Nullable<int> TourPackgeDayId { get; set; }
        //public IEnumerable<SelectListItem> TourPackageDay { get; set; }

        #endregion
    }
}
