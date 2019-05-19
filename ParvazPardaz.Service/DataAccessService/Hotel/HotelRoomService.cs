using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.Service.Contract.Hotel;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using System.Web.Mvc;

namespace ParvazPardaz.Service.DataAccessService.Hotel
{
    public class HotelRoomService : BaseService<HotelRoom>, IHotelRoomService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<HotelRoom> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public HotelRoomService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<HotelRoom>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridHotelRoomViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridHotelRoomViewModel>(_mappingEngine);
        }
        #endregion

        #region GetAllHotelRoomsOfSelectListItem
        public IEnumerable<SelectListItem> GetAllHotelRoomsOfSelectListItem()
        {
            return _dbSet.Where(x => x.IsDeleted == false).Select(r => new SelectListItem() { Text = r.Title, Value = r.Id.ToString() }).AsEnumerable();

        }
        #endregion
    }
}
