using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    public enum ActiveStatus
    {
        [Display(Name = "Inactive", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        Inactive = 0,
        [Display(Name = "Active", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        Active = 1
    }
}
