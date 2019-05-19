using ParvazPardaz.DataAccess.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using System.Linq.Expressions;
using AutoMapper;
using ParvazPardaz.ViewModel;
using System.Web;
using ParvazPardaz.Common.Extension;
using EntityFramework.Extensions;
using System.Threading;
using ParvazPardaz.Service.Contract.Common;

namespace ParvazPardaz.Service.DataAccessService.Common
{
    public class BaseService<TModel> : IBaseService<TModel> where TModel : class
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public BaseService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine/*, IDbSet<TModel> dbSet*/)
        {
            _unitOfWork = unitOfWork;
            _mappingEngine = mappingEngine;
        }
        #endregion

        #region Create
        /// <summary>
        /// ایجاد 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="allowSaveChanges"></param>
        /// <returns>int</returns>
        public virtual TModel Create<TViewModel>(TViewModel viewModel, bool allowSaveAllChanges = true) where TViewModel : class
        {
            var model = (TModel)_mappingEngine.DynamicMap(viewModel, viewModel.GetType(), typeof(TModel));
            _unitOfWork.Set<TModel>().Add(model);
            if (allowSaveAllChanges) _unitOfWork.SaveAllChanges();
            return model;
        }

        #endregion

        #region Create async
        /// <summary>
        /// ایجاد به صورت ناهمرمانی 
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="viewModel"></param>
        /// <returns>Task<int></returns>
        public virtual async Task<TModel> CreateAsync<TViewModel>(TViewModel viewModel, bool allowSaveAllChangesAsync = true) where TViewModel : class
        {
            var model = (TModel)_mappingEngine.DynamicMap(viewModel, viewModel.GetType(), typeof(TModel));
            _unitOfWork.Set<TModel>().Add(model);
            if (allowSaveAllChangesAsync) await _unitOfWork.SaveAllChangesAsync();
            return model;
        }

        #endregion

        #region Update
        /// <summary>
        /// ویرایش
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="allowSaveChanges"></param>
        /// <returns></returns>
        public virtual TModel Update<TViewModel>(TViewModel viewModel, Expression<Func<TModel, bool>> expression, bool allowSaveAllChanges = true) where TViewModel : class
        {
            var model = _unitOfWork.Set<TModel>().FirstOrDefault(expression);
            _mappingEngine.DynamicMap(viewModel, model);
            if (allowSaveAllChanges) _unitOfWork.SaveAllChanges();
            return model;
        }
        #endregion

        #region Update async
        /// <summary>
        /// ویرایش به صورت ناهمزمانی 
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="viewModel"></param>
        /// <returns>Task<int></returns>        
        public virtual async Task<TModel> UpdateAsync<TViewModel>(TViewModel viewModel, Expression<Func<TModel, bool>> expression, bool allowSaveAllChangesAsync = true) where TViewModel : class
        {

            var model = await _unitOfWork.Set<TModel>().FirstAsync(expression);
            _mappingEngine.DynamicMap(viewModel, model);
            if (allowSaveAllChangesAsync) await _unitOfWork.SaveAllChangesAsync();
            return model;
        }
        #endregion

        #region Remove
        public virtual int Delete(Expression<Func<TModel, bool>> where)
        {
            return _unitOfWork.Set<TModel>().Where(where).Delete();
        }

        public virtual async Task<int> DeleteAsync(Expression<Func<TModel, bool>> where)
        {
            return await _unitOfWork.Set<TModel>().Where(where).DeleteAsync();
        }
        #endregion

        #region Remove Logically
        public virtual TModel DeleteLogically(Expression<Func<TModel, bool>> expression, bool allowSaveAllChanges = true)
        {
            var model = (TModel)_unitOfWork.Set<TModel>().FirstOrDefault(expression);
            (model as Model.Entity.Common.BaseEntity).IsDeleted = true;
            (model as Model.Entity.Common.BaseEntity).DeletedDateTime = DateTime.Now;
            if (allowSaveAllChanges) _unitOfWork.SaveAllChanges(updateCommonFields: true);
            return model;
        }
        public virtual async Task<TModel> DeleteLogicallyAsync(Expression<Func<TModel, bool>> expression, bool allowSaveAllChangesAsync = true)
        {
            var model = await _unitOfWork.Set<TModel>().FirstOrDefaultAsync(expression);
            (model as Model.Entity.Common.BaseEntity).IsDeleted = true;
            (model as Model.Entity.Common.BaseEntity).DeletedDateTime = DateTime.Now;
            if (allowSaveAllChangesAsync) await _unitOfWork.SaveAllChangesAsync(updateCommonFields: true);
            return model;
        }

        public virtual async Task<TModel> DeleteLogicallyBigEntityAsync(Expression<Func<TModel, bool>> expression, bool allowSaveAllChangesAsync = true)
        {
            var model = await _unitOfWork.Set<TModel>().FirstOrDefaultAsync(expression);
            (model as Model.Entity.Common.BaseBigEntity).IsDeleted = true;
            (model as Model.Entity.Common.BaseBigEntity).DeletedDateTime = DateTime.Now;
            if (allowSaveAllChangesAsync) await _unitOfWork.SaveAllChangesAsync(updateCommonFields: true);
            return model;
        }

        #endregion

        #region Find
        /// <summary>
        /// پیدا کردن بر اساس آیدی
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async virtual Task<TModel> GetByIdAsync(Expression<Func<TModel, bool>> expression)
        {
            return await _unitOfWork.Set<TModel>().FirstOrDefaultAsync(expression);
        }
        public virtual TModel GetById(Expression<Func<TModel, bool>> expression)
        {
            return _unitOfWork.Set<TModel>().FirstOrDefault(expression);
        }
        #endregion

        #region load
        /// <summary>
        /// لود داده
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TModel> GetAll()
        {
            return _unitOfWork.Set<TModel>().AsQueryable();
        }
        public virtual async Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _unitOfWork.Set<TModel>().ToListAsync(cancellationToken);
        }

        #endregion

        #region Filter
        /// <summary>
        /// فیلتر
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IQueryable<TModel> Filter(Expression<Func<TModel, bool>> where)
        {
            return _unitOfWork.Set<TModel>().Where(where);
        }
        public virtual async Task<IEnumerable<TModel>> FilterAsync(Expression<Func<TModel, bool>> where)
        {
            return await _unitOfWork.Set<TModel>().Where(where).ToListAsync();
        }
        #endregion

        #region GetViewModel
        /// <summary>
        /// جهت تبدیل مدل به ویومدل
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TViewModel GetViewModel<TViewModel>(TModel entity) where TViewModel : class
        {
            return (TViewModel)_mappingEngine.DynamicMap(entity, entity.GetType(), typeof(TViewModel));
        }
        public virtual IQueryable<TViewModel> GetViewModelsByWhere<TViewModel>(Expression<Func<TViewModel, bool>> where) where TViewModel : class
        {
            return _unitOfWork.Set<TModel>().AsNoTracking().ProjectTo<TViewModel>(_mappingEngine).Where(where);
        }
        public virtual TViewModel GetViewModelBySingle<TViewModel>(Expression<Func<TViewModel, bool>> single) where TViewModel : class
        {
            return _unitOfWork.Set<TModel>().AsNoTracking().ProjectTo<TViewModel>(_mappingEngine).FirstOrDefault(single);
        }
        public virtual async Task<TViewModel> GetViewModelAsync<TViewModel>(Expression<Func<TViewModel, bool>> single) where TViewModel : class
        {
            return await _unitOfWork.Set<TModel>().AsNoTracking().ProjectTo<TViewModel>(_mappingEngine).FirstOrDefaultAsync(single);
        }
        #endregion

        #region GetModel
        /// <summary>
        /// جهت تبدبل ویو مدل به مدل
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public virtual TModel GetModel<TViewModel>(TViewModel viewModel) where TViewModel : class
        {
            return (TModel)_mappingEngine.DynamicMap(viewModel, viewModel.GetType(), typeof(TModel));
        }
        #endregion

    }
}
