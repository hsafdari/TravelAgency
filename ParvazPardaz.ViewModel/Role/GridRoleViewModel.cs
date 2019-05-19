using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridRoleViewModel : BaseViewModelOfEntity
    {
        [Display(Name = "RoleName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string RoleName { get; set; }
    }
}
