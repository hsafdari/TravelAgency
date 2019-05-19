using ParvazPardaz.Model.Entity.Book;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Book
{
    public class SelectedAddServConfig : EntityTypeConfiguration<SelectedAddServ>
    {
        public SelectedAddServConfig()
        {
            ToTable("SelectedAddServs", "Book");

            Property(x => x.AddServTitle).IsRequired();
            Property(x => x.Price).IsRequired();
            Property(x => x.Description).IsRequired();
            Property(x => x.Qty).IsRequired();
            Property(x => x.TotalPrice).IsRequired();

            HasRequired(x => x.Order).WithMany(x => x.SelectedAddServs).HasForeignKey(x => x.OrderId).WillCascadeOnDelete(false);
            HasRequired(x => x.AdditionalService).WithMany(x => x.SelectedAddServs).HasForeignKey(x => x.AddServId).WillCascadeOnDelete(false);
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
