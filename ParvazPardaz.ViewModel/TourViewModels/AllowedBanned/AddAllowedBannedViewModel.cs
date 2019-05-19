using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class AddAllowedBannedViewModel : BaseViewModelId
    {
        /// <summary>
        /// نام مورد مجاز یا ممنوع
        /// مجاز یا ممنوع بودن آن در ثبت تور مشخص می شود و  در جدول زیر ذخیره میشود
        /// TourAllowBanned
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [StringLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Title { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        public bool IsActive { get; set; }
    }
}
