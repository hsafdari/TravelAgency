using System;
using System.Collections.Generic;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Users;

namespace ParvazPardaz.Model.Entity.AccessLevel
{
    public class RolePermissionController : BaseBigEntity
    {
        #region ReferenceProperties
        /// <summary>
        /// نقش کاربری
        /// </summary>
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        /// <summary>
        /// دسترسی
        /// </summary>
        public int PermissionId { get; set; }
        public virtual Permission Permission { get; set; }

        /// <summary>
        /// کنترلر
        /// </summary>
        public int ControllerId { get; set; }
        public virtual ControllersList ControllersList { get; set; }
        #endregion
    }
}
