using ParvazPardaz.Model.Entity.Magazine;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Magazine
{
    public class TabMagazineConfig : EntityTypeConfiguration<TabMagazine>
    {
        public TabMagazineConfig()
        {
            ToTable("TabMagazines", "Magazine");

            //Properties
            Property(x => x.Title).IsRequired();
            Property(x => x.Name).IsRequired();
            Property(x => x.Priority).IsRequired();
            Property(x => x.IsActive).IsRequired();
            Property(x => x.CountryId).IsOptional();

            //References
            HasRequired(x => x.Location).WithMany(x => x.TabMagazines).HasForeignKey(x => x.CountryId);

            //Collections
            HasMany(x => x.Groups).WithMany(x => x.TabMagazines).Map(z =>
            {
                z.MapLeftKey("TabMagId");
                z.MapRightKey("GroupId");
                z.ToTable("TabMagazinesPostGroups", "Magazine");
            });

        }
    }
}
