using ParvazPardaz.Model.Book;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Book
{
    public class PaymentUniqueNumberConfig : EntityTypeConfiguration<PaymentUniqueNumber>
    {
        public PaymentUniqueNumberConfig()
        {
            ToTable("PaymentUniqueNumbers", "Book");

            #region Reference navigation
            HasRequired(x => x.Order).WithMany(x => x.PaymentUniqueNumbers).HasForeignKey(x => x.OrderId).WillCascadeOnDelete(false);
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false); 
            #endregion

            #region Concurrency
            Property(x => x.RowVersion).IsRowVersion(); 
            #endregion
        }
    }
}
