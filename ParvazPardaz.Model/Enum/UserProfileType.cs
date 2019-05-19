using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    public enum UserProfileType
    {
        [Display(Name = "Personal", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        Personal = 0,

        [Display(Name = "Company", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        Company = 1
    }
}
