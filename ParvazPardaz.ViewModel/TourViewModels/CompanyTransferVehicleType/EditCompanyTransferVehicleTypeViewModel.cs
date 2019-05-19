using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class EditCompanyTransferVehicleTypeViewModel : BaseViewModelId
    {
        /// <summary>
        /// نام مدل نقلیه
        /// </summary>
        [Display(Name = "ModelName", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [StringLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string ModelName { get; set; }

        /// <summary>
        /// شناسه شرکت حمل و نقل
        /// </summary>
        [Display(Name = "CompanyTransfer", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int CompanyTransferId { get; set; }

        /// <summary>
        /// همه شرکتهای حمل و نقل
        /// </summary>
        public IEnumerable<SelectListItem> CompanyTransferList { get; set; }

        /// <summary>
        /// شناسه نوع وسیله حمل و نقل
        /// </summary>
        [Display(Name = "VehicleType", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int VehicleTypeId { get; set; }

        /// <summary>
        /// همه انواع وسایل حمل و نقل
        /// </summary>
        public IEnumerable<SelectListItem> VehicleTypeList { get; set; }
    }
}
