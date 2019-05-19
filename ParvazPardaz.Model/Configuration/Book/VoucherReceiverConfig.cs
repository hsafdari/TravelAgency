using ParvazPardaz.Model.Entity.Book;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Book
{
    public class VoucherReceiverConfig:EntityTypeConfiguration<VoucherReceiver>
    {
        public VoucherReceiverConfig()
        {
            ToTable("VoucherReceivers", "Book");

            Property(x => x.FullName).IsRequired();
            Property(x => x.Email).IsRequired();
            Property(x => x.Mobile).IsRequired();

            HasRequired(x => x.Order).WithMany(x => x.VoucherReceivers).HasForeignKey(x => x.OrderId).WillCascadeOnDelete(false);
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
