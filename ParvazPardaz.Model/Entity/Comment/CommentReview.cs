using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityType = ParvazPardaz.Model.Entity.Comment;

namespace ParvazPardaz.Model.Entity.Comment
{
    public class CommentReview : BaseEntity
    {
        #region Properties
        /// <summary>
        /// امتیاز داده شده
        /// </summary>
        public decimal Rate { get; set; } 
        #endregion

        #region Reference Navigation
        /// <summary>
        /// شناسه ی دیدگاه
        /// </summary>
        public int CommentId { get; set; }
        public virtual EntityType.Comment Comment { get; set; }

        /// <summary>
        /// شناسه ی مورد نقد و بررسی
        /// </summary>
        public int ReviewId { get; set; }
        public virtual EntityType.Review Review { get; set; }
        #endregion
    }
}
