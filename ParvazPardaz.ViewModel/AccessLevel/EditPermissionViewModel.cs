using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
 
namespace ParvazPardaz.ViewModel
{
    public class EditPermissionViewModel
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "PermissionName", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string PermissionName { get; set; } 

    }
}
