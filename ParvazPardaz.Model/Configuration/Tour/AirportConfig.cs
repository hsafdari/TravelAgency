using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Tour
{
    public class AirportConfig : EntityTypeConfiguration<Airport>
    {
        public AirportConfig()
        {
            ToTable("Airports", "Tour");
            Property(x => x.Title).IsRequired();
            Property(x => x.TitleEn).IsRequired();
            Property(x => x.IataCode).HasMaxLength(10).IsRequired();

            HasRequired(x => x.City).WithMany(x=>x.Airports).HasForeignKey(x => x.CityId).WillCascadeOnDelete(false);

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
           
        }
    }
}
