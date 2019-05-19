using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class EditRoleViewModel : BaseViewModelId
    {
        /// <summary>
        /// نام گروه کاربری
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "RoleName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [StringLength(25, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessageResourceName = "ReqularFormatRoleName", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Name { get; set; }
    }
}
