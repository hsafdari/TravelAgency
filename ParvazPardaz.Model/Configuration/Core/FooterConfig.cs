using ParvazPardaz.Model.Entity.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Core
{
    public class FooterConfig : EntityTypeConfiguration<Footer>
    {
        public FooterConfig()
        {
            ToTable("Footers", "Core");

            Property(x => x.Title).IsRequired();
            Property(x => x.Content).IsRequired();
            Property(x => x.OrderID).IsRequired();
            Property(x => x.IsActive).IsOptional();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();

        }
    }
}
