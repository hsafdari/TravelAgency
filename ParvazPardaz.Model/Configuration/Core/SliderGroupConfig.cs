using ParvazPardaz.Model.Entity.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Core
{
    public class SliderGroupConfig : EntityTypeConfiguration<SliderGroup>
    {
        public SliderGroupConfig()
        {
            ToTable("SliderGroups", "Core");

            Property(x => x.GroupTitle).HasMaxLength(100).IsRequired();
            Property(x => x.Name).HasMaxLength(100).IsRequired();
            Property(x => x.Priority).IsRequired();

            HasMany(x => x.Sliders).WithRequired(x => x.SliderGroup).HasForeignKey(x => x.SliderGroupID).WillCascadeOnDelete(true);

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
