using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    public enum StatusUser
    {
        [Display(Name = "DeActive", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        DeActive = 0,

        [Display(Name = "Active", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        Active = 1
    }
}
