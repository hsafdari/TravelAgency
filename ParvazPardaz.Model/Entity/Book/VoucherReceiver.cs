using System;
using System.Collections.Generic;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Book
{
    public class VoucherReceiver : BaseEntity
    {
        #region Properties
        /// <summary>
        /// نام و نام خانوادگی
        /// </summary>
        public string FullName { get; set; }
        
        /// <summary>
        /// ایمیل
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// موبایل
        /// </summary>
        public string Mobile { get; set; }
        #endregion

        #region Reference navigation properties
        /// <summary>
        /// سفارش
        /// </summary>
        public long OrderId { get; set; }
        public virtual Order Order { get; set; }
        #endregion
    }
}
