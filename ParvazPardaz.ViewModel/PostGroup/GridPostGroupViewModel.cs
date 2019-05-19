using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridPostGroupViewModel : BaseViewModelOfEntity
    {
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Name { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public bool IsActive { get; set; }

        [Display(Name = "Parent", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string Parent { get; set; }



    }
}
