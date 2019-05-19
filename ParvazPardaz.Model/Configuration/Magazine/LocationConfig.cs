using ParvazPardaz.Model.Entity.Magazine;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Magazine
{
    public class LocationConfig : EntityTypeConfiguration<Location>
    {
        public LocationConfig()
        {
            ToTable("Locations", "Magazine");

            Property(x => x.Title).IsRequired().HasMaxLength(25);
            Property(x => x.ENTitle).IsRequired().HasMaxLength(25);
            Property(x => x.URL).IsRequired();
            Property(x => x.SeoText).IsOptional();
            Property(x => x.ImageURL).IsOptional().HasMaxLength(250);

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
