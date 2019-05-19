using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    /// <summary>
    /// اطلاعات نویسنده بلاگ
    /// </summary>
    public class BlogWriterViewModel
    {
        #region Constructor
        public BlogWriterViewModel()
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// نمایه کاربر
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// نام و نام خانوادگی
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// توضیحات
        /// </summary>
        public string Description { get; set; }
        #endregion
    }
}
