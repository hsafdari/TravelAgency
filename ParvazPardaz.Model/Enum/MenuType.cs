using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    // 0 :       when creating submneu
    // 1, 2, 3 : when creating rootmenu
    //public enum MenuType
    //{
    //    Type_0 = 0,
    //    Type_1 = 1,
    //    Type_2 = 2,
    //    Type_3 = 3
    //}

    public enum MenuType
    {
        [Display(Name = "submenu", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        Type_0 = 0,
        [Display(Name = "link", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        Type_1 = 1,
        [Display(Name = "submenuAndImage", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        Type_2 = 2,
        [Display(Name = "submenuAndRootImage", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        Type_3 = 3

        //[Display(Name = "MenuCascadingType", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //MenuCascadingType,

        //[Display(Name = "MenuBrandType", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //MenuBrandType,

        //[Display(Name = "MenuBlockType", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //MenuBlockType,

        //[Display(Name = "MenuPageType", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //MenuPageType,

        //[Display(Name = "MenuSpecialLinkOnLeftType", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //MenuSpecialLinkOnLeftType
    }
}
