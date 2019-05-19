using ParvazPardaz.Model.Entity.Hotel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Hotel
{
    public class HotelFacilityConfig : EntityTypeConfiguration<HotelFacility>
    {
        public HotelFacilityConfig()
        {
            ToTable("HotelFacilities", "Hotel");
            Property(x => x.Title).HasMaxLength(100).IsRequired();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
