using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Enum
{
    /// <summary>
    /// نوع لاگ اعتبار
    /// </summary>
    public enum EnumCreditType
    {
        /// <summary>
        /// نقدی
        /// </summary>
        [Display(Name = "Cash", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        Cash,

        /// <summary>
        /// اعتباری
        /// </summary>
        [Display(Name = "Credit", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        Credit
    }
}
