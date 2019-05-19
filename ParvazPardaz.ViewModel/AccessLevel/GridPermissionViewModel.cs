using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridPermissionViewModel
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "PermissionName", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string PermissionName { get; set; }

    }
}
