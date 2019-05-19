using ParvazPardaz.Model.Book;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Book
{
    public class PaymentLogConfig : EntityTypeConfiguration<PaymentLog>
    {
        public PaymentLogConfig()
        {
            ToTable("PaymentLogs", "Book");

            #region Properties
            Property(x => x.OrderId).IsRequired();
            Property(x => x.PaymentUniqueNumberID).IsRequired();
            Property(x => x.PaymentDate).IsRequired();
            Property(x => x.IsSuccessful).IsRequired();
            Property(x => x.PaymentResponseMessage).IsRequired();
            Property(x => x.PaymentResponseCode).IsRequired();
            Property(x => x.TrackingCode).IsRequired();
            Property(x => x.TotalAmount).IsRequired();
            #endregion

            #region Reference navigation properties
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.Bank).WithMany(x => x.PaymentsLogs).HasForeignKey(x => x.BankId).WillCascadeOnDelete(false);
            #endregion

            #region Concurrency
            Property(x => x.RowVersion).IsRowVersion();
            #endregion
        }
    }
}
