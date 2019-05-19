using ParvazPardaz.Model.Entity.Hotel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Hotel
{
    public class HotelPackageHotelRoomsConfig : EntityTypeConfiguration<HotelPackageHotelRoom>
    {
        public HotelPackageHotelRoomsConfig()
        {
            ToTable("HotelPackageHotelRooms", "Hotel");
            Property(x => x.AdultPrice).IsRequired().HasPrecision(13, 2);
            Property(x => x.ChildPrice).IsRequired().HasPrecision(13, 2);
            Property(x => x.InfantPrice).IsRequired().HasPrecision(13, 2);
            //Property(x => x.OtherCurrencyPrice).IsOptional().HasPrecision(10, 2);
            Property(x => x.AdultOtherCurrencyPrice).IsOptional().HasPrecision(13, 2);
            Property(x => x.ChildOtherCurrencyPrice).IsOptional().HasPrecision(13, 2);
            Property(x => x.InfantOtherCurrencyPrice).IsOptional().HasPrecision(13, 2);
            Property(x => x.NonLimit).IsRequired();
            //Property(x => x.Capacity).IsRequired();
            Property(x => x.AdultCapacity).IsRequired();
            Property(x => x.ChildCapacity).IsRequired();
            Property(x => x.InfantCapacity).IsRequired();
            //Property(x => x.CapacitySold).IsRequired();
            Property(x => x.AdultCapacitySold).IsRequired();
            Property(x => x.ChildCapacitySold).IsRequired();
            Property(x => x.InfantCapacitySold).IsRequired();

            HasOptional(x => x.Currency).WithMany(x => x.HotelPackageHotelRooms).HasForeignKey(x => x.OtherCurrencyId).WillCascadeOnDelete(false);
            HasRequired(x => x.HotelPackage).WithMany(x => x.HotelPackageHotelRooms).HasForeignKey(x => x.HotelPackageId).WillCascadeOnDelete(true);
            HasRequired(x => x.HotelRoom).WithMany(x => x.HotelPackageHotelRooms).HasForeignKey(x => x.HotelRoomId).WillCascadeOnDelete(true);

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
         }
    }
}
