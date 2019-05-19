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
    public class GridCommentViewModel : BaseViewModelOfEntity
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
        public string Email { get; set; }

        /// <summary>
        /// نام کاربر
        /// </summary>
        [Display(Name = "Name", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string Name { get; set; }

        /// <summary>
        /// متن دیدگاه
        /// </summary>
        [Display(Name = "CommentText", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string CommentText { get; set; }

        /// <summary>
        /// مجموع امتیاز
        /// </summary>
        [Display(Name = "Rate", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public decimal Rate { get; set; }

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
        /// تعداد افرادی که امتیاز داده اند
        /// </summary>
        [Display(Name = "RateCount", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public int RateCount { get; set; }

        /// <summary>
        /// مورد تایید؟
        /// </summary>
        [Display(Name = "IsApproved", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public bool IsApproved { get; set; }
        #endregion

        #region Reference navigatiuon properties
        /// <summary>
        /// شناسه جدول پست یا جدول کالا
        /// </summary>
        public int OwnId { get; set; }

        /// <summary>
        /// شناسه جدول پست یا جدول کالا
        /// </summary>
        [Display(Name = "OwnTitle", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string OwnTitle { get; set; }

        /// <summary>
        /// آدرس صفحه کالا یا پست
        /// </summary>
        [Display(Name = "OwnLink", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string OwnLink { get; set; }

        /// <summary>
        /// کامنت برای کدام جدول
        /// </summary>
        [Display(Name = "CommentType", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public CommentType CommentType { get; set; }
        public string CommentTypeTitle
        {
            get
            {
                switch (CommentType)
                {
                    case CommentType.Hotel:
                        return "هتل";

                    case CommentType.Tour:
                        return "تور";

                    case CommentType.Post:
                        return "پست";

                    default:
                        return "";
                }
                //return (CommentType == ParvazPardaz.Model.Enum.CommentType.PostComment ? "پست" : "کالا");
            }
        }
        #endregion

    }
}
