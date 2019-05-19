using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Tour
{
    public class CurrencyConfig : EntityTypeConfiguration<Currency>
    {
        public CurrencyConfig()
        {
            ToTable("Currencies", "Tour");
            Property(x => x.Title).HasMaxLength(25).IsRequired();
            Property(x => x.CurrenySign).HasMaxLength(10).IsRequired();
            Property(x => x.BaseRialPrice).IsOptional();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
