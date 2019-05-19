using Postal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.EmailSendig
{
    public class ResetPasswordViewModel:Email
    {
        /// <summary>
        /// ایمیل مقصد
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// ایمیل مبدا
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// موضوع ایمیل
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// نام کاربری فرستنده ایمیل
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// نام فرستنده ایمیل
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// نام حانوادگی فرستنده ایمیل
        /// </summary>
        public string Family { get; set; }
        /// <summary>
        /// لینک بازیابی رمز عبور
        /// </summary>
        public string ConfirmUrl { get; set; }
    }
    
}
