using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    public enum AddressType
    {
        [Display(Name = "Office", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        Office = 0,

        [Display(Name = "Home", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        Home = 1
    }
}
