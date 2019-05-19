using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Core
{
    [Table("Footers", Schema = "Core")]
    public class Footer : BaseEntity
    {
        public Footer()
        {

        }

        #region Properties
        /// <summary>
        /// عنوان آیتم پانویس
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// متن آیتم پانویس
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// اولویت قرارگیری آیتم پانویس در بین دیگر آیتم های آن
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// فعال/غیرفعال
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// پانویس برای ...
        /// </summary>
        public EnumFooterType FooterType { get; set; }
        #endregion
    }
}
