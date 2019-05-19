using ParvazPardaz.Model.Entity.Book;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Book
{
    public class OrderConfig : EntityTypeConfiguration<Order>
    {
        public OrderConfig()
        {
            ToTable("Orders", "Book");

            Property(x => x.TrackingCode).IsRequired();
            Property(x => x.TotalPrice).IsRequired();
            Property(x => x.TotalDiscountPrice).IsRequired();
            Property(x => x.TotalTaxPrice).IsRequired();
            Property(x => x.TotalPayPrice).IsRequired();  
            Property(x => x.OrderStep).IsRequired();
            Property(x => x.FlightDateTime).IsRequired();
            Property(x => x.TicketReferenceNo).IsRequired();
            Property(x => x.AdultCount).IsRequired();
            Property(x => x.ChildCount).IsRequired();
            Property(x => x.InfantCount).IsRequired();
            Property(x => x.RemainingFlightCapacity).IsRequired();
            Property(x => x.IsSuccessful).IsRequired();

            HasRequired(x => x.AgencySetting).WithMany(x => x.Orders).HasForeignKey(x => x.AgencySettingId).WillCascadeOnDelete(false);
            HasRequired(x => x.OrderType).WithMany(x => x.Orders).HasForeignKey(x => x.OrderTypeId).WillCascadeOnDelete(false);
            HasOptional(x => x.Currency).WithMany(x => x.Orders).HasForeignKey(x => x.CurrencyId).WillCascadeOnDelete(false);
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
