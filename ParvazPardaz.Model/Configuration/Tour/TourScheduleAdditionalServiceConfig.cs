using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Tour
{
    public class TourScheduleAdditionalServiceConfig : EntityTypeConfiguration<TourScheduleAdditionalService>
    {
        public TourScheduleAdditionalServiceConfig()
        {
            ToTable("TourScheduleAdditionalServices", "Tour");
            Property(x => x.Price).IsOptional().HasPrecision(10, 2);
            Property(x => x.Description).IsOptional();

            HasRequired(x => x.TourSchedule).WithMany(x => x.TourScheduleAdditionalServices).HasForeignKey(x => x.TourScheduleId).WillCascadeOnDelete(true);
            HasRequired(x => x.AdditionalService).WithMany(x => x.TourScheduleAdditionalServices).HasForeignKey(x => x.AdditionalServiceId).WillCascadeOnDelete(true);

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
