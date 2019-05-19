using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class EditRequiredDocumentViewModel : BaseViewModelId
    {
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Title { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public bool IsActive { get; set; }
    }
}
