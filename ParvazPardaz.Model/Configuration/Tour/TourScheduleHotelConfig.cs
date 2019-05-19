using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Tour
{
    public class TourScheduleHotelConfig : EntityTypeConfiguration<TourScheduleHotel>
    {
        public TourScheduleHotelConfig()
        {
            ToTable("TourScheduleHotels", "Tour");
            HasRequired(x => x.TourSchedule).WithMany(x => x.TourScheduleHotels).HasForeignKey(x => x.TourScheduleId).WillCascadeOnDelete(true);
            HasRequired(x => x.Hotel).WithMany(x => x.TourScheduleHotels).HasForeignKey(x => x.HotelId).WillCascadeOnDelete(true);

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
