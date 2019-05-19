using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    public enum EnumFooterType
    {
        [Display(Name = "MainFooter", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        Main,

        [Display(Name = "BlogFooter", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        Blog,

        [Display(Name = "Ticket", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        Ticket,

        //[Display(Name = "ShoppingRules", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        //ShoppingRules
    }
}
