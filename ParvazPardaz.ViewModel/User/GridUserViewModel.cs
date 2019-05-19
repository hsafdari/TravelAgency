using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridUserViewModel : BaseViewModelOfEntity
    {
        [Display(Name = "UserName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string UserName { get; set; }
        [Display(Name = "FirstName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string FirstName { get; set; }
        [Display(Name = "LastName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string LastName { get; set; }
        [Display(Name = "FullName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string FullName { get; set; }
        [Display(Name = "IsSystemAccount", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public bool IsSystemAccount { get; set; }
        [Display(Name = "DisplayName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string DisplayName { get; set; }
        [Display(Name = "Email", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Email { get; set; }
        [Display(Name = "PhoneNumber", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string PhoneNumber { get; set; }
        //[Display(Name = "Gender", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        //public string Gender { get; set; }
        /// <summary>
        /// وضعیت کاربر
        /// </summary>
        [Display(Name = "Status", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public StatusUser Status { get; set; }
         [Display(Name = "UserProfiletype", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public UserProfileType UserProfiletype { get; set; }
        //[Display(Name = "RoleNames", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        //public List<string> RoleNames { get; set; }
         /// <summary>
         /// سازمان
         /// </summary>
         [Display(Name = "Organization", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
         public string Organization { get; set; }

         [Display(Name = "NationalCode", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
         public string NationalCode { get; set; }
    }
}
