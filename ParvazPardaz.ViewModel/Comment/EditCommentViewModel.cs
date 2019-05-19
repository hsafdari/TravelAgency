using ParvazPardaz.Model.Entity.Comment;
using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class EditCommentViewModel : BaseViewModelId
    {
        #region Properties
        /// <summary>
        /// موضوع
        /// </summary>
        [Display(Name = "Subject", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string Subject { get; set; }
        /// <summary>
        /// ایمیل
        /// </summary>
        [Display(Name = "Email", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessageResourceName = "IncorrectEmail", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Email { get; set; }
        /// <summary>
        /// نام کاربر
        /// </summary>
        [Display(Name = "Name", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Name { get; set; }
        /// <summary>
        /// متن دیدگاه
        /// </summary>
        [Display(Name = "CommentText", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [AllowHtml]
        [UIHint("TinyMCE_Modern")]
        public string CommentText { get; set; }

        /// <summary>
        /// مجموع امتیاز
        /// </summary>
        [Display(Name = "Rate", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public decimal Rate { get; set; }

        /// <summary>
        /// تعداد افرادی که امتیاز داده اند
        /// </summary>
        [Display(Name = "RateCount", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public int RateCount { get; set; }

        /// <summary>
        /// تعداد پسند
        /// </summary>
        [Display(Name = "Like", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        public Nullable<int> Like { get; set; }

        /// <summary>
        /// تعداد ناپسند
        /// </summary>
        [Display(Name = "DisLike", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        public Nullable<int> DisLike { get; set; }

        /// <summary>
        /// مورد تایید؟
        /// </summary>
        [Display(Name = "IsApproved", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public bool IsApproved { get; set; }
        #endregion

        #region Reference navigatiuon properties
        //<summary>
        //عنوان  پست یا کالا
        //</summary>
        [Display(Name = "OwnTitle", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public int OwnTitle { get; set; }

        /// <summary>
        /// کامنت برای کدام جدول
        /// </summary>
        [Display(Name = "CommentType", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public CommentType CommentType { get; set; }
        #endregion

    }
}
