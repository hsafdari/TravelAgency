using ParvazPardaz.Model.Entity.AccessLevel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.AccessLevel
{
    public class ControllersListConfig : EntityTypeConfiguration<ControllersList>
    {
        #region Constructor
        public ControllersListConfig()
        {
            ToTable("ControllersLists", "AccessLevel");

            Property(x => x.PageName).HasMaxLength(100).IsRequired();
            Property(x => x.PageUrl).HasMaxLength(250).IsRequired();
            Property(x => x.ControllerName).HasMaxLength(100).IsRequired();
            Property(x => x.ActionName).HasMaxLength(20).IsRequired();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
        #endregion
    }
}
