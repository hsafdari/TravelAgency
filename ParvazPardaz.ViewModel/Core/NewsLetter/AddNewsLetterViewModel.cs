using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel.Core
{
    public class AddNewsLetterViewModel : BaseViewModelId
    {
        [MaxLength(150)]
        [Display(Name = "Name", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string Name { get; set; }

        [MaxLength(150)]
        [Display(Name = "Email", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessageResourceName = "InValidEmail", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Email { get; set; }

        [MaxLength(20)]
        [Display(Name = "Mobile", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string Mobile { get; set; }
    }
}
