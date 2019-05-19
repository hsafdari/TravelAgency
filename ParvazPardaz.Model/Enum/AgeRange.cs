using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    public enum AgeRange
    {
        [Display(Name = "Adult", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        Adult = 0,

        [Display(Name = "Child", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        Child,

        [Display(Name = "Infant", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        Infant,
    }
}
