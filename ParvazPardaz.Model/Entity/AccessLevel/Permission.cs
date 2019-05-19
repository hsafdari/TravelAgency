using System;
using System.Collections.Generic;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.AccessLevel
{
    public class Permission : BaseEntity
    {
        #region Properties
        /// <summary>
        /// نام دسترسی
        /// </summary>
        public string PermissionName { get; set; } 
        #endregion

        #region Collection navigation properties
        /// <summary>
        /// این دسترسی برای کدوم نقش ها و به کدوم کنترلرهایی داده شده؟
        /// </summary>
        public virtual ICollection<RolePermissionController> RolePermissionControllers { get; set; }
        #endregion
    }
}
