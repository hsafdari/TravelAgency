using ParvazPardaz.Model.Book;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Book
{
    public class CreditConfig : EntityTypeConfiguration<Credit>
    {
        public CreditConfig()
        {
            ToTable("Credits", "Book");

            #region Properties
            Property(x => x.Amount).IsRequired();
            Property(x => x.CreditType).IsRequired();
            Property(x => x.Description).IsOptional();
            Property(x => x.OrderId).IsOptional();
            Property(x => x.UserId).IsOptional();
            #endregion

            #region Navigation properties
            HasOptional(جدول => جدول.Order).WithMany(جدول => جدول.Credits).HasForeignKey(کلید => کلید.OrderId).WillCascadeOnDelete(false);
            HasOptional(جدول => جدول.UserProfile).WithMany(جدول => جدول.Credits).HasForeignKey(کلید => کلید.UserId).WillCascadeOnDelete(false);
            #endregion

            #region Concurrency
            Property(x => x.RowVersion).IsRowVersion();
            #endregion
        }
    }
}
