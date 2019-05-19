using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Core
{
    public class Support : BaseEntity
    {
        /// <summary>
        /// فعال؟
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        public string UpdateDescription { get; set; }
        /// <summary>
        /// توضیحات فنی مدیریت
        /// </summary>
        public string UpdateAdminDescription { get; set; }
        /// <summary>
        /// نویسنده
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// تاریخ پشتیبانی
        /// </summary>
        public DateTime SupportDateTime { get; set; }
    }
}
