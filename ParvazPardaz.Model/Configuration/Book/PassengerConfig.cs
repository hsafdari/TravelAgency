using ParvazPardaz.Model.Entity.Book;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Book
{
    public class PassengerConfig : EntityTypeConfiguration<Passenger>
    {
        public PassengerConfig()
        {
            ToTable("Passengers", "Book");

            Property(x => x.FirstName).IsRequired();
            Property(x => x.LastName).IsRequired();
            Property(x => x.NationalCode).IsRequired();
            //Property(x => x.Price).IsRequired();
            Property(x => x.TicketNo).IsRequired();
            Property(x => x.Gender).IsRequired();
            Property(x => x.AgeRange).IsRequired();

            HasRequired(x => x.SelectedRoom).WithMany(x => x.Passengers).HasForeignKey(x => x.SelectedRoomId).WillCascadeOnDelete(false);
            //HasRequired(x => x.Order).WithMany(x => x.Passengers).HasForeignKey(x => x.OrderId).WillCascadeOnDelete(false);
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.BirthCountry).WithMany(x => x.Passengers).HasForeignKey(x => x.BirthCountryId).WillCascadeOnDelete(false);
            HasOptional(x => x.PassportExporterCountry).WithMany(x => x.PassportPassengers).HasForeignKey(x => x.PassportExporterCountryId).WillCascadeOnDelete(false);

            //Many-To-Many
            HasMany(x => x.SelectedAddServs).WithMany(x => x.Passengers).Map(z => {
                z.ToTable("SelectedAddServPassengers", "Book");
                z.MapLeftKey("SelectedAddServId");
                z.MapRightKey("PassengerId");
            });

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
