using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityNS = ParvazPardaz.Model.Entity.Book;

namespace ParvazPardaz.Model.Configuration.Book
{
    public class TaskConfig : EntityTypeConfiguration<EntityNS.Task>
    {
        public TaskConfig()
        {
            ToTable("Tasks", "Book");

            Property(x => x.Title).IsRequired();
            Property(x => x.IsDate).IsOptional();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
