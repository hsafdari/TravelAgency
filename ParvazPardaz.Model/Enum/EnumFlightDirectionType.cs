using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    public enum EnumFlightDirectionType
    {
        [Display(Name = "Go", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        Go,

        [Display(Name = "Back", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        Back,
    }
}
