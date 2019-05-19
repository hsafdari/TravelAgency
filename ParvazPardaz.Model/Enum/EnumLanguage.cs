using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    public enum EnumLanguage
    {
        [Display(Name = "Persian", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        Persian = 1,

        [Display(Name = "English", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        English = 2
    }
}
