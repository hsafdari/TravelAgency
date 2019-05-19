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
    public enum OrderState
    {
        [Description("پیش فاکتور")]
        [Display(Name = "preFactor", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [LocalizedDescriptionAttribute("preFactor", typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        preFactor = 0,

        [Description("در حال بررسی")]
        [Display(Name = "pending", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [LocalizedDescriptionAttribute("pending", typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        pending = 1,

        [Description("تسویه شده")]
        [Display(Name = "Factor", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [LocalizedDescriptionAttribute("Factor", typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        Factor = 2,

        [Description("تایید شده")]
        [Display(Name = "Accepted", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [LocalizedDescriptionAttribute("Accepted", typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        Accepted = 3,

        [Description("در حال بسته بندی")]
        [Display(Name = "Packing", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [LocalizedDescriptionAttribute("Packing", typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        Packing = 4,

        [Description("ارسال شده")]
        [Display(Name = "Sended", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [LocalizedDescriptionAttribute("Sended", typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        Sended = 5,

        [Description("رسیده")]
        [Display(Name = "Delivered", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [LocalizedDescriptionAttribute("Delivered", typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        Delivered = 6,

        [Description("لغو شده")]
        [Display(Name = "Cancel", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [LocalizedDescriptionAttribute("Cancel", typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        Cancel = 7

    }
}
