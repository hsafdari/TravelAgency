using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration
{
    public class TourScheduleConfig : EntityTypeConfiguration<TourSchedule>
    {
        public TourScheduleConfig()
        {
            ToTable("TourSchedules", "Tour");
            Property(x => x.FromDate).IsRequired();
            Property(x => x.ToDate).IsRequired();
            Property(x => x.Price).IsOptional().HasPrecision(10, 2);
            Property(x => x.ExpireDate).IsOptional();

            //HasRequired(x => x.Tour).WithMany(x => x.TourSchedules).HasForeignKey(x => x.TourId).WillCascadeOnDelete(true);
            HasRequired(x => x.TourPackage).WithMany(x => x.TourSchedules).HasForeignKey(x => x.TourPackageId).WillCascadeOnDelete(true);
            HasOptional(x => x.Currency).WithMany(x => x.TourSchedules).HasForeignKey(x => x.CurrencyId).WillCascadeOnDelete(true);

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
