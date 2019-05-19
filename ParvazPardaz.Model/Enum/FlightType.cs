using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    public enum FlightType
    {
        [Display(Name = "Charter", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        Charter = 0,

        [Display(Name = "Systematic", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        Systematic
    }
}
