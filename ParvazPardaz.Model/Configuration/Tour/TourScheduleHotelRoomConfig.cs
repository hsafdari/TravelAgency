//using ParvazPardaz.Model.Entity.Tour;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity.ModelConfiguration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ParvazPardaz.Model.Configuration.Tour
//{
//    public class TourScheduleHotelRoomConfig : EntityTypeConfiguration<TourScheduleHotelRoom>
//    {
//        public TourScheduleHotelRoomConfig()
//        {
//            ToTable("TourScheduleHotelRooms", "Tour");
//            Property(x => x.Price).IsRequired().HasPrecision(10, 2);

//            HasRequired(x => x.TourSchedule).WithMany(x => x.TourScheduleHotelRooms).HasForeignKey(x => x.TourScheduleId).WillCascadeOnDelete(true);
//            HasRequired(x => x.HotelRoom).WithMany(x => x.TourScheduleHotelRooms).HasForeignKey(x => x.HotelRoomId).WillCascadeOnDelete(true);

//            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
//            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

//            Property(x => x.RowVersion).IsRowVersion();
//        }
//    }
//}
