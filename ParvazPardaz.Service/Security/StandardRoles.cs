using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Service.Security
{
    public class StandardRoles
    {
        #region DefaultRoles
        public const string SystemAdministrator = "SystemAdministrator";
        public const string PanelUser = "PanelUser";
        public const string Operators = "Operators";
        public const string Customer = "Customer";
        #endregion

        #region GetSysmteRoles
        public static IEnumerable<string> GetSysmteRoles()
        {
            return new List<string>
            {
              SystemAdministrator,
              PanelUser,
              Operators
            };
        }
        #endregion

        //public static IEnumerable<PermissionRecord> SystemRolesWithPermissions
        //{
        //    get
        //    {
        //        if (_rolesWithPermissionsLazy.IsValueCreated)
        //            return _rolesWithPermissionsLazy.Value;
        //        _rolesWithPermissionsLazy = new Lazy<IEnumerable<PermissionRecord>>(GetDefaultRolesWithPermissions);
        //        return _rolesWithPermissionsLazy.Value;
        //    }
        //}
    }
}
