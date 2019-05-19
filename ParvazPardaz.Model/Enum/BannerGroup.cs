using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    public enum BannerGroup
    {
        [Display(Name = "FirstBanner", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        First,

        [Display(Name = "SecondBanner", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        Second,

        [Display(Name = "ThirdBanner", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        Third,

        [Display(Name = "FourthBanner", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        Fourth
    }
}
