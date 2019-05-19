using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    /// <summary>
    /// یکی از موارد نقد و بررسی
    /// </summary>
    public class CommentReviewViewModel
    {
        /// <summary>
        /// شناسه ی مورد نقد و بررسی
        /// </summary>
        public int ReviewId { get; set; }

        /// <summary>
        /// عنوان مورد نقد و بررسی
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// امتیاز مورد نقد و بررسی
        /// </summary>
        public Nullable<decimal> Rate { get; set; }
    }
}
