using ParvazPardaz.Model.Entity.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Product
{
    public class ProductTagProductConfig : EntityTypeConfiguration<ProductTagProduct>
    {
        public ProductTagProductConfig()
        {
            ToTable("ProductTagProducts", "Product");

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //reference navigation properties
            //HasRequired(x => x.Product).WithMany(x => x.ProductTagProducts).HasForeignKey(x => x.ProductId).WillCascadeOnDelete(false);
            HasRequired(x => x.ProductTag).WithMany(x => x.ProductTagProducts).HasForeignKey(x => x.ProductTagId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
