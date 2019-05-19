using ParvazPardaz.Model.Entity.Book;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Book
{
    public class AgencySettingConfig : EntityTypeConfiguration<AgencySetting>
    {
        public AgencySettingConfig()
        {
            ToTable("AgencySettings", "Book");

            Property(x => x.Title).IsRequired();
            Property(x => x.Name).IsRequired();
            Property(x => x.PrintText).IsRequired();
            Property(x => x.ImageUrl).IsRequired();
            Property(x => x.ImageExtension).IsOptional();
            Property(x => x.ImageFileName).IsOptional();
            Property(x => x.ImageSize).IsOptional();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
