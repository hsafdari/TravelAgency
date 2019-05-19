using ParvazPardaz.Model.Entity.AccessLevel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.AccessLevel
{
    public class RolePermissionControllerConfig : EntityTypeConfiguration<RolePermissionController>
    {
        #region Constructor
        public RolePermissionControllerConfig()
        {
            ToTable("RolePermissionControllers", "AccessLevel");

            HasRequired(x => x.Role).WithMany(x => x.RolePermissionControllers).HasForeignKey(x => x.RoleId).WillCascadeOnDelete(false);
            HasRequired(x => x.Permission).WithMany(x => x.RolePermissionControllers).HasForeignKey(x => x.PermissionId).WillCascadeOnDelete(false);
            HasRequired(x => x.ControllersList).WithMany(x => x.RolePermissionControllers).HasForeignKey(x => x.ControllerId).WillCascadeOnDelete(false);
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
        #endregion
    }
}
