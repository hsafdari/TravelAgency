using ParvazPardaz.Model.Entity.Hotel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Hotel
{
    public class HotelGalleryConfig : EntityTypeConfiguration<HotelGallery>
    {
        public HotelGalleryConfig()
        {
            ToTable("HotelGalleries", "Hotel");
            Property(x => x.ImageExtension).IsOptional().HasMaxLength(5);
            Property(x => x.ImageFileName).IsOptional().HasMaxLength(250);
            Property(x => x.ImageUrl).IsOptional().HasMaxLength(300);
            Property(x => x.ImageSize).IsOptional();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
