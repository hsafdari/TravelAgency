using ParvazPardaz.Model.Entity.Hotel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Hotel
{
    public class HotelRoomConfig : EntityTypeConfiguration<HotelRoom>
    {
        public HotelRoomConfig()
        {
            ToTable("HotelRooms", "Hotel");
            Property(x => x.Title).IsRequired().HasMaxLength(100);
            Property(x => x.Priority).IsOptional();
            Property(x => x.HasAdult).IsRequired();
            Property(x => x.HasChild).IsRequired();
            Property(x => x.HasInfant).IsRequired();
            Property(x => x.AdultMaxCapacity).IsRequired();
            Property(x => x.AdultMinCapacity).IsRequired();
            Property(x => x.ChildMaxCapacity).IsRequired();
            Property(x => x.ChildMinCapacity).IsRequired();
            Property(x => x.InfantMaxCapacity).IsRequired();
            Property(x => x.InfantMinCapacity).IsRequired();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
