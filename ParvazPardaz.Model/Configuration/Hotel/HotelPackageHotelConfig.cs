using ParvazPardaz.Model.Entity.Hotel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Hotel
{
    public class HotelPackageHotelConfig : EntityTypeConfiguration<HotelPackageHotel>
    {
        public HotelPackageHotelConfig()
        {
            ToTable("HotelPackageHotels", "Hotel");

            HasRequired(x => x.Hotel).WithMany(x => x.HotelPackageHotels).HasForeignKey(x => x.HotelId).WillCascadeOnDelete(false);
            HasRequired(x => x.HotelPackage).WithMany(x => x.HotelPackageHotels).HasForeignKey(x => x.HotelPackageId).WillCascadeOnDelete(false);
            HasOptional(x => x.HotelBoard).WithMany(x => x.HotelPackageHotels).HasForeignKey(x => x.HotelBoardId).WillCascadeOnDelete(false);

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
