using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    public enum Gender
    {
        [Display(Name = "Man", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        Man = 0,

        [Display(Name = "Woman", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        Woman = 1
    }
}
