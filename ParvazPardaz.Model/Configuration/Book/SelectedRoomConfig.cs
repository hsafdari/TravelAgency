using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Book
{
    public class SelectedRoomConfig : EntityTypeConfiguration<SelectedRoom>
    {
        public SelectedRoomConfig()
        {
            ToTable("SelectedRooms", "Book");

            //Property(x => x.CurrentCapacity).IsRequired();
            Property(x => x.CurrentAdultCapacity).IsRequired();
            Property(x => x.CurrentChildCapacity).IsRequired();
            Property(x => x.CurrentInfantCapacity).IsRequired();
            //Property(x => x.ReserveCapacity).IsRequired();
            Property(x => x.RialPrice).IsRequired();
            //Property(x => x.CurrencyPrice).IsOptional();
            Property(x => x.AdultCurrencyPrice).IsOptional();
            Property(x => x.ChildCurrencyPrice).IsOptional();
            Property(x => x.InfantCurrencyPrice).IsOptional();
            Property(x => x.CurrencySign).HasMaxLength(10).IsOptional();
            Property(x => x.BaseCurrencyToRialPrice).IsOptional();
            //Property(x => x.UnitPrice).IsRequired();
            Property(x => x.AdultUnitPrice).IsRequired();
            Property(x => x.ChildUnitPrice).IsRequired();
            Property(x => x.InfantUnitPrice).IsRequired();

            HasRequired(x => x.SelectedHotelPackage).WithMany(x => x.SelectedRooms).HasForeignKey(x => x.SelectedHotelPackageId).WillCascadeOnDelete(false);
            HasRequired(x => x.HotelRoom).WithMany(x => x.SelectedRooms).HasForeignKey(x => x.HotelRoomId).WillCascadeOnDelete(false);
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
