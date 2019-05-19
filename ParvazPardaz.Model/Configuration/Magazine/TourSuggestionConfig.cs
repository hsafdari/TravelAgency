using ParvazPardaz.Model.Entity.Magazine;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Magazine
{
    public class TourSuggestionConfig : EntityTypeConfiguration<TourSuggestion>
    {
        public TourSuggestionConfig()
        {
            ToTable("TourSuggestion", "Magazine");
            Property(x => x.TourTitle).IsRequired().HasMaxLength(200);
            Property(x => x.TourDate).IsOptional().HasMaxLength(100);
            Property(x => x.AirlineTitle).IsRequired().HasMaxLength(50);
            Property(x => x.TourDuration).IsRequired().HasMaxLength(50);
            Property(x => x.TourPrice).IsRequired().HasMaxLength(25);
            Property(x => x.ImageURL).IsRequired().HasMaxLength(250);
            Property(x => x.NavigationUrl).HasMaxLength(400).IsOptional();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);
            HasRequired(x => x.Locations).WithMany().HasForeignKey(x => x.LocationId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
       
    }
}
