using ParvazPardaz.Model.Entity.Book;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Book
{
    public class SelectedHotelConfig : EntityTypeConfiguration<SelectedHotel>
    {
        public SelectedHotelConfig()
        {
            ToTable("SelectedHotels", "Book");

            Property(x => x.CheckInDateTime).IsRequired();
            Property(x => x.CheckOutDateTime).IsRequired();
            Property(x => x.HotelDescription).IsRequired();

            HasRequired(x=>x.SelectedHotelPackage).WithMany(x=>x.SelectedHotels).HasForeignKey(x=>x.SelectedHotelPackageId).WillCascadeOnDelete(false);
            HasRequired(x=>x.Hotel).WithMany(x=>x.SelectedHotels).HasForeignKey(x=>x.HotelId).WillCascadeOnDelete(false);
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
