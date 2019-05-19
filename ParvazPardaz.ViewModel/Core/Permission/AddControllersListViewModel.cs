using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
 
namespace ParvazPardaz.ViewModel
{
    public class AddControllersListViewModel
    {
        public AddControllersListViewModel()
        {

        }

        public AddControllersListViewModel(List<RoleCheckBoxListViewModel> _roles)
        {
            roles = _roles;
        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "PageName", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string PageName { get; set; } 
                           
        [Display(Name = "PageUrl", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string PageUrl { get; set; } 
                           
        [Display(Name = "ControllerName", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string ControllerName { get; set; } 
                           
        [Display(Name = "ActionName", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string ActionName { get; set; }

        [Display(Name = "PermissionName", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        public string PermissionName { get; set; }

        [Display(Name = "Roles", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        public List<RoleCheckBoxListViewModel> roles { get; set; }
                           
        //[Display(Name = "LastUpdateTime", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //public System.DateTime? LastUpdateTime { get; set; } 
                           
        //[Display(Name = "LastUpdaterUserID", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //public long? LastUpdaterUserID { get; set; } 

        //[Display(Name = "CreatorUserID", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //public long? CreatorUserID { get; set; } 

        //[Display(Name = "RolePermissionControllers", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //public System.Collections.Generic.ICollection<ParvazPardaz.Model.RolePermissionController> RolePermissionControllers { get; set; } 
                                                   
    }
}
