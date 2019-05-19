using ParvazPardaz.Model.Entity.AccessLevel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.AccessLevel
{
    public class PermissionConfig : EntityTypeConfiguration<Permission>
    {
        #region Constructor
        public PermissionConfig()
        {
            ToTable("Permissions", "AccessLevel");

            Property(x => x.PermissionName).HasMaxLength(20).IsRequired();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
        #endregion
    }
}
