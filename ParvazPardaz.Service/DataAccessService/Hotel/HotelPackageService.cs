using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.Contract.Hotel;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace ParvazPardaz.Service.DataAccessService.Hotel
{
    public class HotelPackageService : BaseService<HotelPackage>, IHotelPackageService
    {
        #region Fields


        private readonly IUnitOfWork _unitOfWork;
        private readonly IHotelService _hotelService;
        private readonly IHotelRoomService _hotelRoomService;
        private readonly ITourPackageService _tourPackageService;
        private readonly IDbSet<HotelPackage> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        private readonly HttpContextBase _httpContextBase;
        #endregion

        #region Ctor
        public HotelPackageService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, IHotelService hotelService, HttpContextBase httpContextBase, IHotelRoomService hotelRoomService, ITourPackageService tourPackageService)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<HotelPackage>();
            _hotelService = hotelService;
            _httpContextBase = httpContextBase;
            _hotelRoomService = hotelRoomService;
            _tourPackageService = tourPackageService;
        }
        #endregion

        #region Create
        public HotelPackage CreateHotelPackageWithRoomPrice(HotelPackageViewModel hotelPackage)
        {
            List<HotelPackage> hotelpackageList = new List<HotelPackage>();
            var model = new HotelPackage();
            model.HotelPackageHotelRooms = new List<HotelPackageHotelRoom>();
            model.TourPackageId = hotelPackage.TourPackageId;
            model.TourPackage = _tourPackageService.GetById(x => x.Id == hotelPackage.TourPackageId);
            model.OrderId = hotelPackage.OrderId;
            _dbSet.Add(model);
            _unitOfWork.SaveAllChanges();

            foreach (var item in hotelPackage.HotelsInPackage)
            {
                if (item.HotelId != null && item.HotelId > 0)
                {
                    var hotelpackageHotel = new HotelPackageHotel()
                    {
                        HotelId = item.HotelId,
                        HotelPackageId = model.Id,
                        HotelBoardId = item.HotelBoardId
                    };
                    _unitOfWork.Set<HotelPackageHotel>().Add(hotelpackageHotel);
                }
            }

            foreach (var item in hotelPackage.HotelRoomsInPackage)
            {
                if (item.AdultPrice != null)
                {
                    //var rooms=_hotelRoomService=GetById(x=>x.Id==item.)
                    HotelPackageHotelRoom rooms = new HotelPackageHotelRoom();
                    rooms.HotelRoom = _hotelRoomService.GetById(x => x.Id == item.RoomTypeId);
                    rooms.HotelPackageId = model.Id;
                    rooms.HotelRoomId = item.RoomTypeId;
                    rooms.AdultPrice = item.AdultPrice;
                    rooms.AdultCapacity = item.AdultCapacity;
                    rooms.AdultOtherCurrencyPrice = item.AdultOtherCurrencyPrice;
                    //
                    rooms.ChildPrice = item.ChildPrice;
                    rooms.ChildCapacity = item.ChildCapacity;
                    rooms.ChildOtherCurrencyPrice = item.ChildOtherCurrencyPrice;
                    //
                    rooms.InfantPrice = item.InfantPrice;
                    rooms.InfantCapacity = item.InfantCapacity;
                    rooms.InfantOtherCurrencyPrice = item.InfantOtherCurrencyPrice;

                    rooms.ChildCapacitySold = 0;
                    rooms.InfantCapacitySold = 0;
                    rooms.OtherCurrencyId = item.OtherCurrencyId;
                    model.HotelPackageHotelRooms.Add(rooms);
                }
            }
            _unitOfWork.SaveAllChanges();
            return model;
        }
        #endregion

        #region Edit
        public HotelPackage UpdateHotelPackageWithRoomPrice(HotelPackageViewModel hotelPackageVM)
        {
            //واکشی پکیج تور
            var hotelPackageInDB = _unitOfWork.Set<HotelPackage>().FirstOrDefault(x => x.Id == hotelPackageVM.Id);

            //به روز رسانی ترتیب پکیج
            hotelPackageInDB.OrderId = hotelPackageVM.OrderId;

            //حذف هتل های قبلی
            var hotelPackageHotelList = hotelPackageInDB.HotelPackageHotels.ToList();
            foreach (var item in hotelPackageHotelList)
            {
                _unitOfWork.Set<HotelPackageHotel>().Remove(item);
            }

            //افزودن هتل های ویوومدل
            foreach (var item in hotelPackageVM.HotelsInPackage)
            {
                var hotelpackageHotel = new HotelPackageHotel()
                {
                    HotelId = item.HotelId,
                    HotelPackageId = hotelPackageInDB.Id,
                    HotelBoardId = item.HotelBoardId
                };
                _unitOfWork.Set<HotelPackageHotel>().Add(hotelpackageHotel);
            }

            //حذف قیمت های اتاق های هتل ها
            var roomTypeList = hotelPackageInDB.HotelPackageHotelRooms.ToList();
            hotelPackageInDB.HotelPackageHotelRooms = new HashSet<HotelPackageHotelRoom>();
            foreach (var item in roomTypeList)
            {
                //hotelPackageInDB.HotelPackageHotelRooms.Remove(item);
                _unitOfWork.Set<HotelPackageHotelRoom>().Remove(item);
            }

            //افزودن قیمت های اتاق های هتل ها
            foreach (var item in hotelPackageVM.HotelRoomsInPackage)
            {
                if (item.AdultPrice != null)
                {
                    HotelPackageHotelRoom rooms = new HotelPackageHotelRoom();
                    rooms.HotelRoom = _hotelRoomService.GetById(x => x.Id == item.RoomTypeId);
                    rooms.HotelPackageId = hotelPackageInDB.Id;
                    rooms.HotelRoomId = item.RoomTypeId;
                    rooms.AdultPrice = item.AdultPrice;
                    rooms.ChildPrice = item.ChildPrice;
                    rooms.InfantPrice = item.InfantPrice;
                    //rooms.Capacity = item.Capacity;
                    rooms.AdultCapacity = item.AdultCapacity;
                    rooms.ChildCapacity = item.ChildCapacity;
                    rooms.InfantCapacity = item.InfantCapacity;
                    //rooms.CapacitySold = 0;
                    rooms.AdultCapacitySold = 0;
                    rooms.ChildCapacitySold = 0;
                    rooms.InfantCapacitySold = 0;
                    rooms.OtherCurrencyId = item.OtherCurrencyId;
                    //rooms.OtherCurrencyPrice = item.OtherCurrencyPrice;
                    rooms.AdultOtherCurrencyPrice = item.AdultOtherCurrencyPrice;
                    rooms.ChildOtherCurrencyPrice = item.ChildOtherCurrencyPrice;
                    rooms.InfantOtherCurrencyPrice = item.InfantOtherCurrencyPrice;
                    hotelPackageInDB.HotelPackageHotelRooms.Add(rooms);
                }
            }

            _unitOfWork.SaveAllChanges(false);
            return hotelPackageInDB;
        }
        #endregion

        #region CopyHotelPackagesByTourPackageId
        public TourPackage CopyHotelPackagesByTourPackageId(int fromTourPackageId = 0, int toTourPackageId = 0)
        {
            var fromTourPackage = _tourPackageService.GetById(x => x.Id == fromTourPackageId);
            var fromHotelPackages = fromTourPackage.HotelPackages.Where(x => !x.IsDeleted).ToList();

            var toTourPackage = _unitOfWork.Set<TourPackage>().FirstOrDefault(x => x.Id == toTourPackageId);// _tourPackageService.GetById(x => x.Id == toTourPackageId);
            toTourPackage.HotelPackages = new HashSet<HotelPackage>();
            foreach (var item in fromHotelPackages)
            {
                #region generate newHotelPackage
                var newHotelPackage = new HotelPackage()
                        {
                            HotelPackageHotelRooms = new HashSet<HotelPackageHotelRoom>(),
                            HotelPackageHotels = new HashSet<HotelPackageHotel>(),
                            OrderId = item.OrderId,
                        };

                foreach (var hphr in item.HotelPackageHotelRooms.ToList())
                {
                    var newHphr = new HotelPackageHotelRoom()
                    {
                        //Capacity = hphr.Capacity,
                        AdultCapacity = hphr.AdultCapacity,
                        ChildCapacity = hphr.ChildCapacity,
                        InfantCapacity = hphr.InfantCapacity,
                        //CapacitySold = 0,
                        AdultCapacitySold = 0,
                        ChildCapacitySold = 0,
                        InfantCapacitySold = 0,
                        Currency = hphr.Currency,
                        HotelPackage = newHotelPackage,
                        HotelPackageId = newHotelPackage.Id,
                        HotelRoom = hphr.HotelRoom,
                        HotelRoomId = hphr.HotelRoomId,
                        NonLimit = hphr.NonLimit,
                        OtherCurrencyId = hphr.OtherCurrencyId,
                        //OtherCurrencyPrice = hphr.OtherCurrencyPrice,
                        AdultOtherCurrencyPrice = hphr.AdultOtherCurrencyPrice,
                        ChildOtherCurrencyPrice = hphr.ChildOtherCurrencyPrice,
                        InfantOtherCurrencyPrice = hphr.InfantOtherCurrencyPrice,
                        //Price = hphr.Price
                    };
                    newHotelPackage.HotelPackageHotelRooms.Add(newHphr);
                }

                foreach (var hph in item.HotelPackageHotels.ToList())
                {
                    var newHph = new HotelPackageHotel()
                    {
                        Hotel = hph.Hotel,
                        HotelBoard = hph.HotelBoard,
                        HotelBoardId = hph.HotelBoardId,
                        HotelId = hph.HotelId,
                        HotelPackage = newHotelPackage,
                        HotelPackageId = hph.HotelPackageId
                    };
                    newHotelPackage.HotelPackageHotels.Add(newHph);
                }
                #endregion

                toTourPackage.HotelPackages.Add(newHotelPackage);
            }

            _unitOfWork.SaveAllChanges();

            return toTourPackage;
        }
        #endregion

        #region GetHotelRoomInPackageTable
        /// <summary>
        /// دریافت اطلاعات انواع اتاق موجود در پکیج های هتل
        /// برای ویرایش قیمت و ظرفیت
        /// </summary>
        /// <returns></returns>
        public IQueryable<EditHotelRoomInPackageViewModel> GetHotelRoomInPackageTable()
        {
            var hphrs = _unitOfWork.Set<HotelPackageHotelRoom>().Where(x => !x.IsDeleted && !x.HotelPackage.IsDeleted && !x.HotelPackage.TourPackage.IsDeleted && !x.HotelPackage.TourPackage.Tour.IsDeleted && x.HotelPackage.TourPackage.Tour.Recomended).Include(x => x.HotelPackage).Include(x => x.HotelRoom).OrderByDescending(x => x.CreatorDateTime).OrderBy(x => x.HotelPackageId);
            return hphrs.AsNoTracking().ProjectTo<EditHotelRoomInPackageViewModel>(_mappingEngine);
        }
        #endregion

        #region InlineUpdate
        public bool InlineUpdate(int id, string property, string value)
        {
            var item = _unitOfWork.Set<ParvazPardaz.Model.Entity.Hotel.HotelPackageHotelRoom>().FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                switch (property)
                {
                    //case "TotalCapacity":
                    //    {
                    //        item.Capacity = Int32.Parse(value);
                    //        break;
                    //    }
                    case "AdultCapacity":
                        {
                            item.AdultCapacity = Int32.Parse(value);
                            break;
                        }
                    case "ChildCapacity":
                        {
                            item.ChildCapacity = Int32.Parse(value);
                            break;
                        }
                    case "InfantCapacity":
                        {
                            item.InfantCapacity = Int32.Parse(value);
                            break;
                        }
                    case "AdultPrice":
                        {
                            item.AdultPrice = decimal.Parse(value);
                            break;
                        }
                    case "ChildPrice":
                        {
                            item.ChildPrice = decimal.Parse(value);
                            break;
                        }
                    case "InfantPrice":
                        {
                            item.InfantPrice = decimal.Parse(value);
                            break;
                        }
                    case "AdultOtherCurrencyPrice":
                        {
                            item.AdultOtherCurrencyPrice = decimal.Parse(value);
                            break;
                        }
                    case "ChildOtherCurrencyPrice":
                        {
                            item.ChildOtherCurrencyPrice = decimal.Parse(value);
                            break;
                        }
                    case "InfantOtherCurrencyPrice":
                        {
                            item.InfantOtherCurrencyPrice = decimal.Parse(value);
                            break;
                        }
                }

            }
            _unitOfWork.SaveAllChanges(true);
            return true;
        }
        #endregion
    }
}
