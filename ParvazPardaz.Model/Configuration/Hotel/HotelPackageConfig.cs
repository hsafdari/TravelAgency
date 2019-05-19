using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Hotel
{
    public class HotelPackageConfig : EntityTypeConfiguration<HotelPackage>
    {
        public HotelPackageConfig()
        {
            ToTable("HotelPackage", "Hotel");
            Property(x => x.OrderId);
            HasRequired(x => x.TourPackage).WithMany(x => x.HotelPackages).HasForeignKey(x => x.TourPackageId).WillCascadeOnDelete(false);

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
