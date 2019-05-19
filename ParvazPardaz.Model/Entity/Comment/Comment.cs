using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NS = ParvazPardaz.Model.Entity;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.Model.Configuration.Comment;
using EntityType = ParvazPardaz.Model.Entity.Hotel;

namespace ParvazPardaz.Model.Entity.Comment
{
    /// <summary>
    /// جدول دیدگاه کاربران
    /// </summary>
    public class Comment : BaseEntity
    {
        #region Properties
        /// <summary>
        /// موضوع
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// ایمیل
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// نام کاربر
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// متن دیدگاه
        /// </summary>
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
        /// کامنت برای کدام جدول
        /// </summary>
        public CommentType CommentType { get; set; }

        /// <summary>
        /// مورد تایید؟
        /// </summary>
        public bool IsApproved { get; set; }
        #endregion

        #region Reference navigatiuon properties
        /// <summary>
        /// شناسه دیدگاه والد
        /// </summary>
        public Nullable<int> ParentId { get; set; }
        public virtual Comment CommentParent { get; set; }

        /// <summary>
        /// شناسه جدول هتل یا ...
        /// </summary>
        public int OwnId { get; set; }
        //public virtual EntityType.Hotel Hotel { get; set; }
        //public virtual NS.Product.Product Product { get; set; }
        //public virtual NS.Post.Post Post { get; set; }
        #endregion

        #region Collection navigation property
        public virtual ICollection<Comment> CommentChildren { get; set; }
        public virtual ICollection<CommentReview> CommentReviews { get; set; }
        #endregion
    }
}
