using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    public enum BusinessPersonality
    {
        [Display(Name = "NaturalPersonality", ResourceType = typeof(ParvazPardaz.Resource.Commercial.Commercials))]
        Natural = 1,

        [Display(Name = "LegalPersonality", ResourceType = typeof(ParvazPardaz.Resource.Commercial.Commercials))]
        Legal = 2
    }

}
