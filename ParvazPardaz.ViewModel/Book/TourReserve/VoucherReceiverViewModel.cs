using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    /// <summary>
    /// دریافت کنندگان اطلاعات تور
    /// </summary>
    public class VoucherReceiverViewModel
    {
        #region Constructor
        public VoucherReceiverViewModel()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// آرایه ی نام و نام خانودگی
        /// </summary>
        [Display(Name = "FullName", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public String FullName { get; set; }

        /// <summary>
        /// آرایه ی ایمیل
        /// </summary>
        [Display(Name = "Email", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public String Email { get; set; }

        /// <summary>
        /// آرایه ی موبایل
        /// </summary>
        [Display(Name = "Mobile", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        //[RegularExpression("^[0-9]*$", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public String Mobile { get; set; }
        #endregion

        #region SectionIndex
        public Nullable<int> SectionIndex { get; set; } 
        #endregion
    }
}
