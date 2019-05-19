using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Payment.Infrastructure.Enum
{
    /// <summary>
    /// جدول وضعیت پرداخت بانک سامان
    /// </summary>
    public enum SBPaymentStatus
    {
        /// <summary>
        /// کاربر انصراف داده است
        /// </summary>
        [Display(Name = "CanceledByUser", ResourceType = typeof(ParvazPardaz.Payment.Infrastructure.Resource.BankResource))]
        CanceledByUser = 1,

        /// <summary>
        /// پرداخت با موفقیت انجام شد
        /// </summary>
        [Display(Name = "OK", ResourceType = typeof(ParvazPardaz.Payment.Infrastructure.Resource.BankResource))]
        OK = 2,

        /// <summary>
        /// پرداخت انجام نشد
        /// </summary>
        [Display(Name = "Failed", ResourceType = typeof(ParvazPardaz.Payment.Infrastructure.Resource.BankResource))]
        Failed = 3,

        /// <summary>
        /// کاربر در بازه زمانی تعیین شده پاسخی ارسال نکرده است
        /// </summary>
        [Display(Name = "SessionIsNull", ResourceType = typeof(ParvazPardaz.Payment.Infrastructure.Resource.BankResource))]
        SessionIsNull = 4,

        /// <summary>
        /// پارامترهای ارسالی نامعتبر است
        /// </summary>
        [Display(Name = "InvalidParameters", ResourceType = typeof(ParvazPardaz.Payment.Infrastructure.Resource.BankResource))]
        InvalidParameters = 5,

        /// <summary>
        /// در موارد چند تراکنشی، اطلاعات موفقیت از طریق سرویس دیگری ارسال می شود(مخصوص پذیرنده های خاص(
        /// </summary>
        [Display(Name = "MultiStateTxn", ResourceType = typeof(ParvazPardaz.Payment.Infrastructure.Resource.BankResource))]
        MultiStateTxn = 6,

        /// <summary>
        /// در موارد چند تراکنشی، تمام تراکنش ها پرداخت ناموفق داشتند
        /// </summary>
        [Display(Name = "TotalFailed", ResourceType = typeof(ParvazPardaz.Payment.Infrastructure.Resource.BankResource))]
        TotalFailed = 7
    }
}
