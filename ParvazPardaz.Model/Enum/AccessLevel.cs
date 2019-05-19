using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    public enum AccessLevel
    {
        [Display(Name = "general", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        general = 0,

        [Display(Name = "members", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        members,

        [Display(Name = "admins", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        admins,

        [Display(Name = "vipusers", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        vipusers
    }
}
