using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Core
{
    public class ContactUsConfig : EntityTypeConfiguration<ContactUs>
    {
        public ContactUsConfig()
        {
            ToTable("ContactUses", "Core");

            Property(x => x.Title).IsRequired();
            Property(x => x.Name).IsRequired();
            Property(x => x.Email).IsRequired();
            Property(x => x.CellPhone).IsRequired();
            Property(x => x.Content).IsRequired();

            HasRequired(x => x.Department).WithMany(x => x.ContactUses).HasForeignKey(x => x.DepartmentID).WillCascadeOnDelete(false);
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();

        }
    }
}
