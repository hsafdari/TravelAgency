using ParvazPardaz.Model.Entity.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Core
{
    public class NewsLetterConfig : EntityTypeConfiguration<Newsletter>
    {
        public NewsLetterConfig()
        {
            ToTable("NewsLetters", "Core");

            Property(x => x.Name).HasMaxLength(150).IsOptional();
            Property(x => x.Email).HasMaxLength(150).IsRequired();
            Property(x => x.Mobile).HasMaxLength(20).IsOptional();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
