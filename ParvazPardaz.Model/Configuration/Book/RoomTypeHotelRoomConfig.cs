using ParvazPardaz.Model.Entity.Book;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Book
{
    public class RoomTypeHotelRoomConfig : EntityTypeConfiguration<RoomTypeHotelRoom>
    {
        public RoomTypeHotelRoomConfig()
        {
            ToTable("RoomTypeHotelRooms", "Book");

            Property(x => x.MaximumCapacity).IsRequired();

            HasRequired(x => x.HotelRoom).WithMany(x => x.RoomTypeHotelRooms).HasForeignKey(x => x.HotelRoomId).WillCascadeOnDelete(false);
            HasRequired(x => x.RoomType).WithMany(x => x.RoomTypeHotelRooms).HasForeignKey(x => x.RoomTypeId).WillCascadeOnDelete(false);
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
