using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    public enum EnumUpdatePriceType
    {
        [Display(Name = "Numeric", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        Numeric,

        [Display(Name = "Percent", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        Percent,
    }
}
