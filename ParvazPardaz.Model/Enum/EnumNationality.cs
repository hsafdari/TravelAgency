using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    public enum EnumNationality
    {
        [Display(Name = "Iranian", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        Iranian,

        [Display(Name = "NonIranian", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        NonIranian
    }
}
