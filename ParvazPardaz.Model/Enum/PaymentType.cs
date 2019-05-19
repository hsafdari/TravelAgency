using ParvazPardaz.Common.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    public enum PaymentType
    {
        [Display(Name = "Online", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [LocalizedDescriptionAttribute("Online", typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        Online = 0,
        [Display(Name = "InPlace", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [LocalizedDescriptionAttribute("InPlace", typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        InPlace = 1//,
        //[Display(Name = "ToAccount", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //[LocalizedDescriptionAttribute("ToAccount", typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //ToAccount = 2,
        //[Display(Name = "ToCart", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //[LocalizedDescriptionAttribute("ToCart", typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //ToCart = 3
    }
}
