using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data.Entity;
using ParvazPardaz.Model.Configuration;
using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Entity.Users;
using RefactorThis.GraphDiff;
using System.Web;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Model.Configuration.Users;
using ParvazPardaz.ViewModel;
using System.Data.Entity.Validation;

namespace ParvazPardaz.DataAccess.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>, IUnitOfWork
    {
        #region Ctor
        public ApplicationDbContext()
            : base("ApplicationDbContext")
        { }
        #endregion

        #region Override OnModelCreating
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                throw new ArgumentNullException();

            modelBuilder.Ignore<BaseEntity>();

            modelBuilder.Configurations.AddFromAssembly(typeof(UserConfig).Assembly);
            LoadEntities(typeof(User).Assembly, modelBuilder, "ParvazPardaz.Model.Entity");
        }
        #endregion

        #region IUnitOfWork
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }


        public int SaveAllChanges(bool updateCommonFields = true)
        {
            if (updateCommonFields) UpdateCommonFields();
            //return base.SaveChanges();

            try
            {
                return base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        public Task<int> SaveAllChangesAsync(bool updateCommonFields = true)
        {
            if (updateCommonFields) UpdateCommonFields();
            return base.SaveChangesAsync();
        }
        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void MarkAsDetached<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Detached;
        }

        public void MarkAsAdded<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Added;
        }

        public void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public T Update<T>(T entity, System.Linq.Expressions.Expression<Func<RefactorThis.GraphDiff.IUpdateConfiguration<T>, object>> mapping) where T : class, new()
        {
            return this.UpdateGraph(entity, mapping);
        }

        public void AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            ((DbSet<TEntity>)Set<TEntity>()).AddRange(entities);
        }
        #endregion

        #region Private Methods
        private static void LoadEntities(Assembly asm, DbModelBuilder modelBuilder, string nameSpace)
        {
            var entityTypes = asm.GetTypes()
                .Where(type => type.BaseType != null &&
                               type.Namespace == nameSpace &&
                               type.BaseType == null)
                .ToList();

            entityTypes.ForEach(modelBuilder.RegisterEntityType);
        }

        private void UpdateCommonFields()
        {
            var DateTime = System.DateTime.Now;
            var UserId = HttpContext.Current.Request.GetUserId();
            var UserIp = HttpContext.Current.Request.GetIp();
            var UserAgent = HttpContext.Current.Request.GetBrowser();

            foreach (var entry in this.ChangeTracker.Entries<BaseEntity>())
            {
                // Note: You must add a reference to assembly : System.Data.Entity
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatorDateTime = DateTime;
                        entry.Entity.CreatorUserId = UserId;
                        entry.Entity.CreatorUserIpAddress = UserIp;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifierDateTime = DateTime;
                        entry.Entity.ModifierUserId = UserId;
                        entry.Entity.ModifierUserIpAddress = UserIp;
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        entry.Entity.IsDeleted = true;
                        entry.Entity.DeletedDateTime = DateTime;
                        entry.State = EntityState.Modified;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            foreach (var entry in this.ChangeTracker.Entries<BaseBigEntity>())
            {
                // Note: You must add a reference to assembly : System.Data.Entity
                switch (entry.State)
                {
                    case EntityState.Added:
                        //این شرط رو برای این گذاشتم که تفاضل تاریخ ایجاد سفارش و تاریخ مهلت پرداختش 
                        //دقیقا برابر مهلت زمانی پرداخت در وب.کانفیگ باشه
                        //اگر اینجا تاریخ ایجاد رو پر کنیم ، اونوقت مهلت پرداخت دقیقا همون در نمیاد
                        if (entry.Entity.CreatorDateTime == null)
                        {
                            entry.Entity.CreatorDateTime = DateTime;
                        }
                        entry.Entity.CreatorUserId = UserId;
                        entry.Entity.CreatorUserIpAddress = UserIp;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifierDateTime = DateTime;
                        entry.Entity.ModifierUserId = UserId;
                        entry.Entity.ModifierUserIpAddress = UserIp;
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        entry.Entity.IsDeleted = true;
                        entry.Entity.DeletedDateTime = DateTime;
                        entry.State = EntityState.Modified;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        #endregion
    }
}
