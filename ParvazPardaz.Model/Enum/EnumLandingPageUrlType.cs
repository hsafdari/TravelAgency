using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    /// <summary>
    /// آدرس لندینگ پیج تور ، از چه نوعی است؟
    /// </summary>
    public enum EnumLandingPageUrlType
    {
        [Display(Name = "GeneralTour", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        GeneralTour = 0,

        [Display(Name = "DiscountedTour", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        DiscountedTour
    }
}
