using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class AddPostGroupViewModel : BaseViewModelId
    {
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        
        public string Name { get; set; }
        [Display(Name = "EnglishTitle", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Remote("CheckURL", "PostGroup")]
        [RegularExpression(@"^[^<>{}\[\],*?+\\/.؟]+$", ErrorMessage = "کاراکتر ممنوعه")]
        public string Title { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public bool IsActive { get; set; }

        [Display(Name = "Parent", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public Nullable<int> ParentId { get; set; }

        public IEnumerable<SelectListItem> PostGroupList { get; set; }

    }
}
