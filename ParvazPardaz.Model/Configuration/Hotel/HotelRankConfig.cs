using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Hotel
{
    class HotelRankConfig : EntityTypeConfiguration<Entity.Hotel.HotelRank>
    {
        public HotelRankConfig()
        {
            ToTable("HotelRanks", "Hotel");
            Property(x => x.Title).HasMaxLength(100).IsRequired();
            Property(x => x.Icon).HasMaxLength(300).IsOptional();
            Property(x => x.OrderId).IsRequired();
            Property(x => x.ImageExtension).IsOptional().HasMaxLength(5);
            Property(x => x.ImageFileName).IsOptional().HasMaxLength(250);
            Property(x => x.ImageSize).IsOptional();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
