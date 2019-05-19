using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.Model.Entity.Comment;

namespace ParvazPardaz.ViewModel
{
    public class ClientCommentViewModel : BaseViewModelOfEntity
    {
        public ClientCommentViewModel()
        {
            this.CommentChildren = new List<ClientCommentViewModel>();
            this.CommentReviews = new List<CommentReviewViewModel>();
            Like = 0;
            DisLike = 0;
        }

        #region Properties

        /// <summary>
        /// موضوع
        /// </summary>
        [Display(Name = "Subject", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
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
        public string CommentText { get; set; }

        /// <summary>
        /// مجموع امتیاز
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// تعداد افرادی که امتیاز داده اند
        /// </summary>
        public int RateCount { get; set; }

        /// <summary>
        /// تعداد پسند
        /// </summary>
        public Nullable<int> Like { get; set; }

        /// <summary>
        /// تعداد ناپسند
        /// </summary>
        public Nullable<int> DisLike { get; set; }

        /// <summary>
        /// مورد تایید؟
        /// </summary>
        public bool IsApproved { get { return false; } }
        #endregion

        #region User's Comment Rate From Cookie - deactive
        /// <summary>
        /// امتیاز کامنت توسط کاربر جاری ، اگر در کوکی باشد
        /// </summary>
        public decimal CookieCommentRate { get; set; }
        #endregion

        #region Reference navigatiuon properties
        /// <summary>
        /// شناسه دیدگاه والد
        /// </summary>
        [ForeignKey("CommentParent")]
        public Nullable<int> ParentId { get; set; }
        public virtual ClientCommentViewModel CommentParent { get; set; }

        /// <summary>
        /// شناسه کالا
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int OwnId { get; set; }
        /// <summary>
        /// کامنت برای کدام جدول
        /// </summary>
        public CommentType CommentType { get; set; }
        #endregion

        #region CommentReviews
        public List<CommentReviewViewModel> CommentReviews { get; set; }
        #endregion

        #region collection navigation
        public virtual ICollection<ClientCommentViewModel> CommentChildren { get; set; }
        #endregion

    }
}
