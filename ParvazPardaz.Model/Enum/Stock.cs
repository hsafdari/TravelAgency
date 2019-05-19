using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    public enum Stock
    {
        [Display(Name = "NotInStock", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        NotInStock = 0,

        [Display(Name = "InStock", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        InStock = 1
    }
}
