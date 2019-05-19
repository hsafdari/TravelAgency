using System;
using System.Collections.Generic;
using System.Data.Entity;
using ParvazPardaz.Model.Entity.Common;
using AutoMapper;
using ParvazPardaz.ViewModel;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.Service.DataAccessService.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Book;
using ParvazPardaz.TravelAgency.UI.Services.Interface.Book;
using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Users;

namespace ParvazPardaz.TravelAgency.UI.Services.Service.Book
{
    public class CreditService : BaseService<Credit>, ICreditService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public CreditService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region GetForGrid
        //public IQueryable<GridCreditViewModel> GetViewModelForGrid()
        //{
        //    return _dbSet.Where(w => !w.IsDeleted).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
        //                                                  .AsNoTracking().ProjectTo<GridCreditViewModel>(_mappingEngine);
        //}
        #endregion

        #region LogCreditAfterPayment
        public void LogCreditAfterPayment(Order newOrder)
        {
            var identityName = System.Web.HttpContext.Current.User.Identity.Name;
            var loggedInUserId = _unitOfWork.Set<User>().FirstOrDefault(x=>x.UserName.Equals(identityName)).Id;

            #region لاگ پرداخت اعتباری
            var newCredit = new Credit()
            {
                //وقتی پرداخت می کنه از اعتبارش کم می شه. در واقع منفی می خوره
                Amount = -Math.Abs(newOrder.TotalPayPrice),
                CreditType = Model.Enum.EnumCreditType.Credit,
                OrderId = newOrder.Id,
                Description = "پرداخت اعتباری",
                UserId = loggedInUserId
            };
            _unitOfWork.Set<Credit>().Add(newCredit);
            #endregion

            _unitOfWork.SaveAllChanges();
        }
        #endregion
    }
}

