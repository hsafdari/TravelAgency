using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Tour
{
    public class RequiredDocumentConfig : EntityTypeConfiguration<RequiredDocument>
    {
        public RequiredDocumentConfig()
        {
            ToTable("RequiredDocuments", "Tour");

            Property(x => x.Title).IsRequired();
            Property(x => x.IsActive).IsRequired();

            HasMany(x => x.Tours).WithMany(x => x.RequiredDocuments).Map(x =>
            {
                x.ToTable("TourRequiredDocuments", "Tour");
                x.MapLeftKey("RequiredDocumentId");
                x.MapRightKey("TourId");
            });

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
