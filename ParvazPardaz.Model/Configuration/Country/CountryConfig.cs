using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Country
{
    public class CountryConfig : EntityTypeConfiguration<Entity.Country.Country>
    {
        public CountryConfig()
        {
            ToTable("Countries", "Country");

            Property(x => x.ImageExtension).IsOptional().HasMaxLength(5);
            Property(x => x.ImageFileName).IsOptional().HasMaxLength(250);
            Property(x => x.ImageUrl).IsOptional().HasMaxLength(300);
            Property(x => x.ImageSize).IsOptional();
            Property(x => x.Title).IsRequired().HasMaxLength(25);
            Property(x => x.ENTitle).IsRequired().HasMaxLength(25);
            Property(x => x.SeoText).IsOptional();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();

        }
    }
}
