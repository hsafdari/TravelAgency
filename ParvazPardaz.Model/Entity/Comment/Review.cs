using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Enum;
using System.ComponentModel.DataAnnotations;
using ParvazPardaz.Model.Configuration.Comment;


namespace ParvazPardaz.Model.Entity.Comment
{
    /// <summary>
    /// نقد و بررسی
    /// </summary>
    public class Review : BaseEntity
    {
        #region Properties
        /// <summary>
        /// عنوان آیتم نقد و بررسی
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// آیتم نقد و بررسی برای
        /// </summary>
        public ReviewType ReviewType { get; set; }
        /// <summary>
        /// فعال؟
        /// </summary>
        public bool IsActive { get; set; }
        #endregion

        #region Collection navigation properties
        public virtual ICollection<CommentReview> CommentReviews { get; set; }
        #endregion
    }
}
