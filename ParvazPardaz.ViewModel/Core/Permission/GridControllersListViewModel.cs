using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridControllersListViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "PageName", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        public string PageName { get; set; }

        [Display(Name = "PageUrl", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        public string PageUrl { get; set; }

        [Display(Name = "Permission", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        public string Permission { get; set; }

        [Display(Name = "Roles", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        public List<string> Roles { get; set; }

        //[Display(Name = "LastUpdateTime", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        //public System.DateTime? LastUpdateTime { get; set; }  

        //[Display(Name = "LastUpdaterUserID", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        //public long? LastUpdaterUserID { get; set; }  

        //[Display(Name = "CreatorUserID", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        //public long? CreatorUserID { get; set; }  

        //[Display(Name = "RolePermissionControllers", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        //public System.Collections.Generic.ICollection<ParvazPardaz.Model.RolePermissionController> RolePermissionControllers { get; set; }  

    }
}
