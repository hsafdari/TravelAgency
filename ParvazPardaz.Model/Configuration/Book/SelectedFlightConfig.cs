using ParvazPardaz.Model.Entity.Book;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Book
{
    public class SelectedFlightConfig : EntityTypeConfiguration<SelectedFlight>
    {
        public SelectedFlightConfig()
        {
            ToTable("SelectedFlights", "Book");

            Property(x => x.AirlineIATACode).HasMaxLength(20).IsRequired();
            Property(x => x.FlightNo).HasMaxLength(10).IsRequired();
            Property(x => x.FlightDateTime).IsRequired();
            Property(x => x.FromIATACode).HasMaxLength(5).IsRequired();
            Property(x => x.ToIATACode).HasMaxLength(5).IsRequired();
            Property(x => x.FlightType).IsOptional();
            Property(x => x.BaggageAmount).HasMaxLength(20).IsOptional();

            HasOptional(x => x.VehicleTypeClass).WithMany(x => x.SelectedFlights).HasForeignKey(x => x.VehicleTypeClassId).WillCascadeOnDelete(false);
            HasRequired(x => x.Order).WithMany(x => x.SelectedFlights).HasForeignKey(x => x.OrderId).WillCascadeOnDelete(false);
            HasRequired(x => x.TourSchedule).WithMany(x => x.SelectedFlights).HasForeignKey(x => x.TourScheduleId).WillCascadeOnDelete(false);
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.CompanyTransfer).WithMany(x => x.SelectedFlights).HasForeignKey(x => x.CompanyTransferId).WillCascadeOnDelete(false);
            
            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
