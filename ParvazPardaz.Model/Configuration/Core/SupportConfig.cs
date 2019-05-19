using ParvazPardaz.Model.Entity.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Core
{
    public class SupportConfig : EntityTypeConfiguration<Support>
    {
        public SupportConfig()
        {
            ToTable("Supports","Core");

            Property(x => x.Author).IsRequired();
            Property(x => x.UpdateDescription).IsRequired();
            Property(x => x.UpdateAdminDescription).IsOptional();
            Property(x => x.IsActive).IsRequired();
            Property(x => x.SupportDateTime).IsRequired();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
