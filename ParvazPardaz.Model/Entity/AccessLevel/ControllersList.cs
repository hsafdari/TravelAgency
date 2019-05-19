using System;
using System.Collections.Generic;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.AccessLevel
{
    public class ControllersList : BaseEntity
    {
        public ControllersList()
        {
            this.RolePermissionControllers = new List<RolePermissionController>();
        }
        #region Properties
        /// <summary>
        /// عنوان صفحه
        /// </summary>
        public string PageName { get; set; }

        /// <summary>
        /// آدرس صفحه
        /// </summary>
        public string PageUrl { get; set; }

        /// <summary>
        /// کدوم کنترلر
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// کدوم اکشن
        /// </summary>
        public string ActionName { get; set; }
        #endregion

        #region Collection navigation properties
        /// <summary>
        /// چه نقشهایی ، چه دسترسی هایی به این کنترلر دارند؟
        /// </summary>
        public virtual ICollection<RolePermissionController> RolePermissionControllers { get; set; } 
        #endregion
    }
}
