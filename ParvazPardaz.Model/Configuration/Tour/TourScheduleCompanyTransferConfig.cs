using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Tour
{
    public class TourScheduleCompanyTransferConfig : EntityTypeConfiguration<TourScheduleCompanyTransfer>
    {
        public TourScheduleCompanyTransferConfig()
        {
            ToTable("TourScheduleCompanyTransfers", "Tour");
            Property(x => x.StartDateTime).IsRequired();
            Property(x => x.EndDateTime).IsRequired();
            Property(x => x.Capacity).IsOptional();
            //Property(x => x.FlightClass);
            Property(x => x.FlightNumber).HasMaxLength(5);
            Property(x => x.Description).HasMaxLength(400);
            Property(x => x.DestinationAirportId).IsOptional();
            Property(x => x.FromAirportId).IsOptional();
            Property(x => x.BaggageAmount).HasMaxLength(20).IsOptional();
            ///           
            HasOptional(x => x.VehicleTypeClass).WithMany(x => x.TourScheduleCompanyTransfers).HasForeignKey(x => x.VehicleTypeClassId).WillCascadeOnDelete(false);
            HasRequired(x => x.TourSchedule).WithMany(x => x.TourScheduleCompanyTransfers).HasForeignKey(x => x.TourScheduleId).WillCascadeOnDelete(true);
            HasRequired(x => x.CompanyTransfer).WithMany(x => x.TourScheduleCompanyTransfers).HasForeignKey(x => x.CompanyTransferId).WillCascadeOnDelete(true);

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
