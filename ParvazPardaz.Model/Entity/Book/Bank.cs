using System;
using System.Collections.Generic;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Book
{
    /// <summary>
    /// بانک
    /// </summary>
    public class Bank : BaseEntity
    {
        #region Properties
        /// <summary>
        /// آدرس نمایه بانک
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// عنوان انگلیسی
        /// </summary>
        public string EnTitle { get; set; }
        
        /// <summary>
        /// عنوان فارسی
        /// </summary>
        public string FaTitle { get; set; }
        
        /// <summary>
        /// فعال؟
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// آیا به درگاه بانک برود؟
        /// </summary>
        public bool IsRedirectToBank { get; set; }
        
        /// <summary>
        /// اولویت نمایش
        /// </summary>
        public int Priority { get; set; }
        
        /// <summary>
        /// شناسه ی ترمینال دریافتی از بانک
        /// </summary>
        public string BankTerminalId { get; set; }

        /// <summary>
        /// نام کاربری برای درگاه پرداخت در صورت لزوم
        /// </summary>
        public string BankUserName { get; set; }

        /// <summary>
        /// کلمه عبور برای درگاه پرداخت در صورت لزوم
        /// </summary>
        public string BankPassword { get; set; }
        
        /// <summary>
        /// متن درگاه پرداخت دریافتی از بانک
        /// یا هر توضیح ممکن دیگری      
        /// </summary>
        public string Description { get; set; }

        ///// <summary>
        ///// آدرس فایل پیوست
        ///// </summary>
        //public string AttachFileUrl { get; set; }
        #endregion

        #region Collection navigation property
        /// <summary>
        /// لاگ های پرداخت از طریق این بانک
        /// </summary>
        public virtual ICollection<PaymentLog> PaymentsLogs { get; set; }
        #endregion
    }
}
