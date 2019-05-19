using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Tour
{
    public class TourProgramActivityConfig : EntityTypeConfiguration<TourProgramActivity>
    {
        public TourProgramActivityConfig()
        {
            ToTable("TourProgramActivities", "Tour");
            Property(x => x.Description).IsOptional().HasMaxLength(500);
            Property(x => x.TransferVehicleInfo).IsOptional().HasMaxLength(250);
            Property(x => x.Price).IsOptional().HasPrecision(10, 2);

            HasRequired(x => x.TourProgram).WithMany(x => x.TourProgramActivities).HasForeignKey(x => x.TourProgramId).WillCascadeOnDelete(true);
            HasRequired(x => x.Activity).WithMany(x => x.TourProgramActivities).HasForeignKey(x => x.ActivityId).WillCascadeOnDelete(true);

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
