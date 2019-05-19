using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Tour
{
    public class TourPackageConfig : EntityTypeConfiguration<TourPackage>
    {
        public TourPackageConfig()
        {
            ToTable("TourPackage", "Tour");
            Property(x => x.Title).HasMaxLength(300);
            Property(x => x.DateTitle).HasMaxLength(50);
            Property(x => x.Code).HasMaxLength(50);
            Property(x => x.FromPrice).HasMaxLength(50);
            Property(x => x.OfferPrice).HasMaxLength(50);
            Property(x => x.Priority);
            HasRequired(x => x.Tour).WithMany(x => x.TourPackages).HasForeignKey(x => x.TourId).WillCascadeOnDelete(false);
            HasOptional(x => x.TourPackageDay).WithMany(x => x.TourPackages).HasForeignKey(x => x.TourPackgeDayId).WillCascadeOnDelete(false);

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
