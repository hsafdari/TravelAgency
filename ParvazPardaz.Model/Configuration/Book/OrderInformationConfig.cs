using ParvazPardaz.Model.Entity.Book;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Book
{
    public class OrderInformationConfig : EntityTypeConfiguration<OrderInformation>
    {
        public OrderInformationConfig()
        {
            ToTable("OrderInformations", "Book");
            Property(x => x.Id).HasColumnOrder(1);
            Property(x => x.Title).IsRequired();
            Property(x => x.NationalCode).HasMaxLength(10);
            Property(x => x.NationalityId).IsOptional();
            //1:1 navigation
            HasRequired(x => x.Order).WithOptional(x => x.OrderInformation).WillCascadeOnDelete(false);

            //reference navigation
            HasRequired(x => x.User).WithMany(x => x.OrderInformations).HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
            HasOptional(x => x.City).WithMany(x => x.OrderInformations).HasForeignKey(x => x.CityId).WillCascadeOnDelete(false);
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.Country).WithMany(x => x.OrderInformations).HasForeignKey(x => x.NationalityId).WillCascadeOnDelete(false);
            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
