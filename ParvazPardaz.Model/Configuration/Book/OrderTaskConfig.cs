using ParvazPardaz.Model.Entity.Book;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Book
{
    public class OrderTaskConfig : EntityTypeConfiguration<OrderTask>
    {
        public OrderTaskConfig()
        {
            ToTable("OrderTasks", "Book");

            Property(x => x.TaskValue).IsRequired();

            HasRequired(x => x.Task).WithMany(x => x.OrderTasks).HasForeignKey(x => x.TaskId).WillCascadeOnDelete(false);
            HasRequired(x => x.Order).WithMany(x => x.OrderTasks).HasForeignKey(x => x.OrderId).WillCascadeOnDelete(false);
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
