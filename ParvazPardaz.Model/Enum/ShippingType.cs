using ParvazPardaz.Common.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ParvazPardaz.Model.Enum
{
    public enum ShippingType
    {
        [Display(Name = "Post", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [LocalizedDescriptionAttribute("Post", typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        Post = 0,

        [Display(Name = "motorcycleDelivery", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [LocalizedDescriptionAttribute("motorcycleDelivery", typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        motorcycleDelivery = 1,

        [Display(Name = "InPlace", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [LocalizedDescriptionAttribute("InPlace", typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        InPlace = 2
    }
}
