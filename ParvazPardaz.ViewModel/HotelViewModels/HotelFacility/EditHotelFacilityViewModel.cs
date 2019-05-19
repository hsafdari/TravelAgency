using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
   public class EditHotelFacilityViewModel:BaseViewModelId
    {
        /// <summary>
        /// عنوان امکانات هتل
        /// </summary>
        /// 
       [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
       [StringLength(25, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Title { get; set; }
    }
}
