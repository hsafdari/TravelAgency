using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParvazPardaz.Service.Contract.Common
{
    public interface IBaseService<TModel> where TModel : class
    {
        #region Create
        /// <summary>
        /// ایجاد 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="allowSaveChanges"></param>
        /// <returns>int</returns>
        TModel Create<TViewModel>(TViewModel viewModel, bool allowSaveAllChanges = true) where TViewModel : class;
        #endregion

        #region Create Async
        /// <summary>
        /// ایجاد به صورت ناهمزمانی 
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="viewModel"></param>
        /// <returns>Task<int></returns>
        Task<TModel> CreateAsync<TViewModel>(TViewModel viewModel, bool allowSaveAllChangesAsync = true) where TViewModel : class;
        #endregion

        #region Update
        /// <summary>
        /// ویرایش
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="allowSaveChanges"></param>
        /// <returns></returns>
        TModel Update<TViewModel>(TViewModel viewModel, Expression<Func<TModel, bool>> expression, bool allowSaveAllChanges = true) where TViewModel : class;

        #endregion

        #region Update Async
        /// <summary>
        /// ویرایش به صورت ناهمزمانی 
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="viewModel"></param>
        /// <returns>Task<int></returns>        
        Task<TModel> UpdateAsync<TViewModel>(TViewModel viewModel, Expression<Func<TModel, bool>> expression, bool allowSaveAllChangesAsync = true) where TViewModel : class;
        #endregion

        #region Remove
        /// <summary>
        /// حذف به صورت فیزیکی
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>اگر موفقیت آمیز بود 1 در غیر این صورت 0</returns>
        int Delete(Expression<Func<TModel, bool>> where);
        /// <summary>
        /// حذف به صورت فیزیکی و به صورت نا همزمانی
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(Expression<Func<TModel, bool>> where);

        #endregion

        #region Remove Logically
        /// <summary>
        /// حذف به صورت منطقی
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        TModel DeleteLogically(Expression<Func<TModel, bool>> expression, bool allowSaveAllChanges = true);
        /// <summary>
        /// حذف به صورت منطقی و به صورت نا همزمانی
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<TModel> DeleteLogicallyAsync(Expression<Func<TModel, bool>> expression, bool allowSaveAllChangesAsync = true);
        /// <summary>
        /// حذف به صورت منطقی و به صورت نا همزمانی
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="allowSaveAllChangesAsync"></param>
        /// <returns></returns>
        Task<TModel> DeleteLogicallyBigEntityAsync(Expression<Func<TModel, bool>> expression, bool allowSaveAllChangesAsync = true);
        #endregion

        #region Find
        /// <summary>
        /// پیدا کردن
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TModel> GetByIdAsync(Expression<Func<TModel, bool>> expression);

        TModel GetById(Expression<Func<TModel, bool>> expression);

        #endregion

        #region load
        /// <summary>
        /// لود داده
        /// </summary>
        /// <returns></returns>
        IQueryable<TModel> GetAll();
        /// <summary>
        /// لود داده به صورت نا همزمانی
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));


        #endregion

        #region Filter
        /// <summary>
        /// فیلتر
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IQueryable<TModel> Filter(Expression<Func<TModel, bool>> where);
        /// <summary>
        /// فیلتر به صورت نا همزمانی
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<IEnumerable<TModel>> FilterAsync(Expression<Func<TModel, bool>> where);

        #endregion

        #region GetViewModel
        /// <summary>
        /// جهت تبدیل مدل به ViewModel
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        TViewModel GetViewModel<TViewModel>(TModel entity) where TViewModel : class;
        /// <summary>
        /// جهت تبدیل لیستی از مدل ها به ویو مدل Where
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        IQueryable<TViewModel> GetViewModelsByWhere<TViewModel>(Expression<Func<TViewModel, bool>> where) where TViewModel : class;
        /// <summary>
        /// جهت تبدیل مدل به ویو مدل SingleOrDefault
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="single"></param>
        /// <returns></returns>
        TViewModel GetViewModelBySingle<TViewModel>(Expression<Func<TViewModel, bool>> single) where TViewModel : class;
        /// <summary>
        /// جهت تبدیل مدل به ویو مدل به صورت نا همزمان SingleOrDefaultAsync
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="single"></param>
        /// <returns></returns>
        Task<TViewModel> GetViewModelAsync<TViewModel>(Expression<Func<TViewModel, bool>> single) where TViewModel : class;
        #endregion

        #region GetModel
        /// <summary>
        /// حهت تبدیل ویو مدل به مدل
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        TModel GetModel<TViewModel>(TViewModel viewModel) where TViewModel : class;

        #endregion
    }
}
