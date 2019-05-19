using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Tour
{
    public class CompanyTransferConfig : EntityTypeConfiguration<CompanyTransfer>
    {
        public CompanyTransferConfig()
        {
            ToTable("CompanyTransfers", "Tour");
            Property(x => x.Title).IsRequired().HasMaxLength(100);
            Property(x => x.TitleEn).IsRequired().HasMaxLength(100);
            Property(x => x.Address).IsOptional().HasMaxLength(50);
            Property(x => x.Tel).IsOptional().HasMaxLength(15);
            Property(x => x.ImageExtension).IsOptional().HasMaxLength(5);
            Property(x => x.ImageFileName).IsOptional().HasMaxLength(250);
            Property(x => x.ImageUrl).IsOptional().HasMaxLength(300);
            Property(x => x.ImageSize).IsOptional();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
