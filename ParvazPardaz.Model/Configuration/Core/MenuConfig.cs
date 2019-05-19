using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Core;

namespace ParvazPardaz.Model.Configuration.Core
{
    public class MenuConfig : EntityTypeConfiguration<Menu>
    {
        public MenuConfig()
        {
            ToTable("Menus", "Core");
            Property(x => x.MenuTitle).HasMaxLength(150).IsRequired();
            Property(x => x.MenuUrl).HasMaxLength(350).IsOptional();
            Property(x => x.Target).HasMaxLength(10).IsOptional();

            HasRequired(x => x.MenuGroup).WithMany(x => x.Menus).HasForeignKey(x => x.MenuGroupId).WillCascadeOnDelete(true);
            HasOptional(x => x.MenuParent).WithMany(x => x.MenuChilds).HasForeignKey(x => x.MenuParentId).WillCascadeOnDelete(false);


            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
