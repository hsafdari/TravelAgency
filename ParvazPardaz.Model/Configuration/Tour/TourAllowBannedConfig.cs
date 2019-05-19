using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Tour
{
    public class TourAllowBannedConfig : EntityTypeConfiguration<TourAllowBanned>
    {
        public TourAllowBannedConfig()
        {
            ToTable("TourAllowBans", "Tour");

            HasRequired(x => x.Tour).WithMany(x => x.TourAllowBans).HasForeignKey(x => x.TourId).WillCascadeOnDelete(true);
            HasRequired(x => x.AllowedBanned).WithMany(x => x.TourAllowBans).HasForeignKey(x => x.AllowedBannedId).WillCascadeOnDelete(true);

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
