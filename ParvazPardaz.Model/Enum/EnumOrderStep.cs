using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    public enum EnumOrderStep
    {
        [Display(Name = "AreBooking", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        AreBooking = 0,

        [Display(Name = "ProvisionalBooking", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        ProvisionalBooking,

        [Display(Name = "Successful", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        Successful,

        [Display(Name = "Unsuccessful", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        Unsuccessful,

        [Display(Name = "Cancel", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        Cancel,

        [Display(Name = "Retrieval", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        Retrieval
    }
}
