using ParvazPardaz.Model.Entity.Book;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Book
{
    public class SelectedHotelPackageConfig : EntityTypeConfiguration<SelectedHotelPackage>
    {
        public SelectedHotelPackageConfig()
        {
            ToTable("SelectedHotelPackages", "Book");

            Property(x => x.SelectedRoomCount).IsRequired();       
            Property(x => x.AdultCount).IsRequired();
            Property(x => x.ChildCount).IsRequired();
            Property(x => x.InfantCount).IsRequired();
            Property(x => x.TotalAdultPrice).IsRequired();
            Property(x => x.TotalChildPrice).IsRequired();
            Property(x => x.TotalInfantPrice).IsRequired();

            HasRequired(x => x.HotelPackage).WithMany(x => x.SelectedHotelPackages).HasForeignKey(x => x.HotelPackageId).WillCascadeOnDelete(false);
            HasRequired(x => x.Order).WithMany(x => x.SelectedHotelPackages).HasForeignKey(x => x.OrderId).WillCascadeOnDelete(false);
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
