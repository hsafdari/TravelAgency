using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Tour
{
    public class TourLandingPageUrlConfig : EntityTypeConfiguration<TourLandingPageUrl>
    {
        public TourLandingPageUrlConfig()
        {
            ToTable("TourLandingPageUrls", "Tour");

            Property(x => x.Title).HasMaxLength(400).IsRequired();
            Property(x => x.URL).HasMaxLength(400).IsRequired();
            Property(x => x.Description).IsOptional();
            Property(x => x.IsAvailable).IsRequired();

            //Reference navigation
            HasRequired(x => x.City).WithMany(x => x.TourLandingPageUrls).HasForeignKey(x => x.CityId).WillCascadeOnDelete(false);
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
