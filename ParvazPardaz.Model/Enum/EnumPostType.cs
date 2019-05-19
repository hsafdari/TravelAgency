using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    public enum EnumPostType
    {
        [Display(Name = "Post", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        Post,

        [Display(Name = "ClientPage", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        ClientPage
    }
}
