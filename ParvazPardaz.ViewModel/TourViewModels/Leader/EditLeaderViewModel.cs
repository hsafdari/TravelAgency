using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class EditLeaderViewModel : BaseViewModelId
    {
        /// <summary>
        /// نام 
        /// </summary>
        [Display(Name = "FirstName", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [StringLength(25, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        [Display(Name = "LastName", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [StringLength(25, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string LastName { get; set; }
        /// <summary>
        /// جنسیت
        /// </summary>
        [Display(Name = "Gender", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public Gender Gender { get; set; }
        /// <summary>
        /// شماره تلفن
        /// </summary>
        [Display(Name = "Tel", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [StringLength(15, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Tel { get; set; }
        /// <summary>
        /// شماره موبایل 
        /// </summary>
        [Display(Name = "Mobile", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [StringLength(15, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Mobile { get; set; }
    }
}
