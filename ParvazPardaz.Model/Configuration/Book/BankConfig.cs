using ParvazPardaz.Model.Book;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Book
{
    public class BankConfig : EntityTypeConfiguration<Bank>
    {
        public BankConfig()
        {
            ToTable("Banks", "Book");

            #region Propertirs
            Property(x => x.LogoUrl).IsRequired();
            Property(x => x.EnTitle).IsRequired();
            Property(x => x.FaTitle).IsRequired();
            Property(x => x.IsActive).IsRequired();
            Property(x => x.Priority).IsRequired();
            Property(x => x.BankTerminalId).IsRequired();
            Property(x => x.BankUserName).IsRequired();
            Property(x => x.BankPassword).IsRequired();
            Property(x => x.Description).IsOptional();
            //Property(x => x.AttachFileUrl).IsOptional();
            #endregion

            #region Reference navigation properties
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);
            #endregion

            #region Concurrency
            Property(x => x.RowVersion).IsRowVersion();
            #endregion
        }
    }
}
