using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    public enum CommentType
    {
        [Display(Name = "Hotel", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        Hotel = 0,

        [Display(Name = "Tour", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        Tour,

        [Display(Name = "Post", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        Post
    }
}
