using DataTables.Mvc;
using ParvazPardaz.Common.Controller;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.Service.Contract.Country;
using ParvazPardaz.Service.Contract.Hotel;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.Service.Security;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;
using System.Web.Script.Serialization;
using Infrastructure;
namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class HotelPackageController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITourPackageService _tourPackageService;
        private readonly IHotelService _hotelService;
        private readonly ICityService _cityService;
        private readonly IHotelPackageService _hotelPackageService;
        private readonly IHotelRoomService _hotelRoomService;
        private readonly ICurrencyService _currencyService;
        private readonly IHotelBoardService _hotelBoardService;
        private readonly ITourService _tourService;

        private const string SectionId = "Section";
        #endregion

        #region	Ctor
        public HotelPackageController(IUnitOfWork unitOfWork, ICityService cityService, ITourPackageService tourPackageService, IHotelService hotelService, IHotelRoomService hotelRoomService, IHotelPackageService hotelPackageService, ICurrencyService currencyService, IHotelBoardService hotelBoardService, ITourService tourService)
        {
            _unitOfWork = unitOfWork;
            _cityService = cityService;
            _tourPackageService = tourPackageService;
            _hotelService = hotelService;
            _hotelRoomService = hotelRoomService;
            _hotelPackageService = hotelPackageService;
            _hotelBoardService = hotelBoardService;
            _currencyService = currencyService;
            _tourService = tourService;
        }
        #endregion

        #region Create
        public PartialViewResult Create(int? tourPackageId)
        {
            ViewBag.City = _cityService.GetAllCityOfSelectListItem();
            ViewBag.OtherCurrencyDDL = _currencyService.GetAllCurrenciesOfSelectListItem();
            ViewBag.RoomTypeDDL = _hotelRoomService.GetAllHotelRoomsOfSelectListItem();
            ViewBag.HotelBoardDDL = _hotelBoardService.GetDDL();
            ViewBag.Hotel = new List<SelectListItem>();
            var tourPackage = _tourPackageService.GetById(t => t.Id == tourPackageId);

            var hotelPachageViewModel = new HotelPackageViewModel()
            {
                TourId = tourPackage.TourId,
                TourPackageId = tourPackageId.Value,
                CRUDMode = CRUDMode.Create,
                HotelRoomsInPackage = new List<HotelRoomsInPackageViewModel>(),
                ListView = tourPackage.HotelPackages.Where(x => !x.IsDeleted).Any() ? tourPackage.HotelPackages.Where(x => !x.IsDeleted).Select(hotelPackages => GetListView(hotelPackages)).ToList() : null
                //ListView = tourPackage.HotelPackages.SelectMany(x => x.HotelPackageHotels.Select(y => y.HotelPackage)).Any() ? tourPackage.HotelPackages.Select(x => x.HotelPackageHotels.Select(y => y.HotelPackage)).Select(hotelpackages => GetListView(hotelpackages)).ToList() : null
            };
            hotelPachageViewModel.HotelRoomsInPackage.Add(new HotelRoomsInPackageViewModel());

            ViewBag.Tours = _tourService.GetTourDDL();
            var firstTour = _tourService.Filter(x => !x.IsDeleted && x.Recomended).FirstOrDefault();
            ViewBag.TourPackages = new SelectList(_tourPackageService.GetAll().Where(x => !x.IsDeleted && x.TourId == firstTour.Id).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");


            return PartialView("_Create", hotelPachageViewModel);
        }

        [HttpPost]
        public ActionResult Create(HotelPackageViewModel viewModel)
        {
            ViewBag.OtherCurrencyDDL = _currencyService.GetAllCurrenciesOfSelectListItem();
            ViewBag.RoomTypeDDL = _hotelRoomService.GetAllHotelRoomsOfSelectListItem();
            ViewBag.Tours = _tourService.GetTourDDL();
            var firstTour = _tourService.Filter(x => !x.IsDeleted && x.Recomended).FirstOrDefault();
            ViewBag.TourPackages = new SelectList(_tourPackageService.GetAll().Where(x => !x.IsDeleted && x.TourId == firstTour.Id).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");

            if (!viewModel.HotelsInPackage.Any(x => x.HotelId == 0))
            {
                if (ModelState.IsValid)
                {
                    var newHotelPackage = _hotelPackageService.CreateHotelPackageWithRoomPrice(viewModel);
                    return PartialView("_ListViewHotelPackage", GetListView(newHotelPackage));
                }
            }
            return View(viewModel);
        }
        #endregion

        #region CopyHotelPackages
        /// <summary>
        /// کپی پکیج های هتل، از پکیج توری دیگر به پکیج تور فعلی
        /// </summary>
        /// <param name="fromTourPackageId">شناسه ی پکیج تور انتخاب شده</param>
        /// <param name="toTourPackageId">شناسه ی پکیج تور فعلی</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CopyHotelPackages(int fromTourPackageId, int toTourPackageId)
        {
            var toTourPackage = _hotelPackageService.CopyHotelPackagesByTourPackageId(fromTourPackageId, toTourPackageId);

            #region listOfHotelPackages
            var listOfHotelPackages = toTourPackage.HotelPackages.Where(x => !x.IsDeleted).Select(x => new ListViewHotelPackageViewModel()
            {

                Id = x.Id,
                OrderId = x.OrderId,
                TourPackageId = x.TourPackageId,
                TourPackageTitle = x.TourPackage.Title,
                SectionId = SectionId + x.Id,

                hotelsInPackage = x.HotelPackageHotels.Select(y => y.Hotel).Where(h => !h.IsDeleted).Select(h => new HotelsInPackageViewModel
                {
                    CityId = h.CityId,
                    CityTitle = h.City.Title,
                    Location = h.Location,
                    Summary = h.Summary,
                    HotelId = h.Id,
                    HotelTitle = h.Title,
                    TourPackageId = toTourPackageId

                }).ToList(),

                hotelRoomsInPackage = x.HotelPackageHotelRooms.Where(hr => !hr.IsDeleted).Select(hr => new HotelRoomsInPackageViewModel
                {
                    AdultPrice = hr.AdultPrice,
                    ChildPrice=hr.ChildPrice,
                    InfantPrice=hr.InfantPrice,
                    //Capacity = hr.Capacity,
                    AdultCapacity = hr.AdultCapacity,
                    ChildCapacity = hr.ChildCapacity,
                    InfantCapacity = hr.InfantCapacity,
                    //صفدری نیاز به بازبینی دارد
                    AdultCapacitySold=0,
                    InfantCapacitySold=0,
                    ChildCapacitySold = 0,
                    //OtherCurrencyPrice = hr.OtherCurrencyPrice,
                    AdultOtherCurrencyPrice = hr.AdultOtherCurrencyPrice,
                    ChildOtherCurrencyPrice = hr.ChildOtherCurrencyPrice,
                    InfantOtherCurrencyPrice = hr.InfantOtherCurrencyPrice,
                    OtherCurrencyId = hr.OtherCurrencyId,
                    RoomTypeId = hr.HotelRoomId,
                    Title = hr.HotelRoom.Title,
                    Id = hr.Id,
                }).ToList(),
            });
            #endregion

            return PartialView("~/Areas/Admin/Views/HotelPackage/_ListViewHotelPackageCopied.cshtml", listOfHotelPackages.ToList());
        }
        #endregion

        #region FindHotelByCityId
        public JsonResult FindHotelByCityId(int id)
        {
            return Json(_hotelService.FindHotelByCityId(id), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region RefreshPartialView
        public PartialViewResult RefreshPartialView(int TourPackageId)
        {
            ViewBag.City = _cityService.GetAllCityOfSelectListItem();
            ViewBag.Hotel = new List<SelectListItem>();
            ViewBag.OtherCurrencyDDL = _currencyService.GetAllCurrenciesOfSelectListItem();
            ViewBag.RoomTypeDDL = _hotelRoomService.GetAllHotelRoomsOfSelectListItem();
            ViewBag.HotelBoardDDL = _hotelBoardService.GetDDL();
            ViewBag.Tours = _tourService.GetTourDDL();
            var firstTour = _tourService.Filter(x => !x.IsDeleted && x.Recomended).FirstOrDefault();
            ViewBag.TourPackages = new SelectList(_tourPackageService.GetAll().Where(x => !x.IsDeleted && x.TourId == firstTour.Id).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");

            //var tourPackage = _tourPackageService.GetById(t => t.Id == TourPackageId);
            var hotelPachageViewModel = new HotelPackageViewModel()
            {
                TourPackageId = TourPackageId,
                CRUDMode = CRUDMode.Create,
                HotelRoomsInPackage = new List<HotelRoomsInPackageViewModel>()
                //ListView = tourPackage.HotelPackages.Where(x => !x.IsDeleted).Any() ? tourPackage.HotelPackages.Where(x => !x.IsDeleted).Select(tourPackages => GetListView(tourPackages)).ToList() : null
            };
            hotelPachageViewModel.HotelRoomsInPackage.Add(new HotelRoomsInPackageViewModel());

            return PartialView("_AddHotelPackage", hotelPachageViewModel);
        }
        #endregion

        #region GetListView
        public ListViewHotelPackageViewModel GetListView(HotelPackage hotelPackage)
        {
            var q = new ListViewHotelPackageViewModel()
            {
                Id = hotelPackage.Id,

                OrderId = hotelPackage.OrderId,
                TourPackageId = hotelPackage.TourPackageId,
                TourPackageTitle = hotelPackage.TourPackage.Title,
                SectionId = SectionId + hotelPackage.Id,

                hotelRoomsInPackage = hotelPackage.HotelPackageHotelRooms.Select(hr => new HotelRoomsInPackageViewModel
                {
                    AdultPrice = hr.AdultPrice,
                    ChildPrice=hr.ChildPrice,
                    InfantPrice=hr.InfantPrice,
                    //Capacity = hr.Capacity,
                    AdultCapacity = hr.AdultCapacity,
                    ChildCapacity = hr.ChildCapacity,
                    InfantCapacity = hr.InfantCapacity,
                    //CapacitySold = hr.CapacitySold,
                    AdultCapacitySold = hr.AdultCapacitySold,
                    ChildCapacitySold = hr.ChildCapacitySold,
                    InfantCapacitySold = hr.InfantCapacitySold,
                    //OtherCurrencyPrice = hr.OtherCurrencyPrice,
                    AdultOtherCurrencyPrice = hr.AdultOtherCurrencyPrice,
                    ChildOtherCurrencyPrice = hr.ChildOtherCurrencyPrice,
                    InfantOtherCurrencyPrice = hr.InfantOtherCurrencyPrice,
                    OtherCurrencyId = hr.OtherCurrencyId,
                    RoomTypeId = hr.HotelRoomId,
                    Title = hr.HotelRoom.Title,
                    Id = hr.Id

                }).ToList(),
            };

            foreach (var item in hotelPackage.HotelPackageHotels)
            {
                var hotel = _hotelService.GetById(x => x.Id == item.HotelId);
                var hotelInPackage = new HotelsInPackageViewModel
                {
                    CityId = hotel.CityId,
                    CityTitle = hotel.City.Title,
                    Location = hotel.Location,
                    Summary = hotel.Summary,
                    HotelId = hotel.Id,
                    HotelTitle = hotel.Title
                };
                q.hotelsInPackage.Add(hotelInPackage);
            }

            return q;
        }
        #endregion

        #region GetHotelPartial
        [HttpGet]
        public PartialViewResult GetHotelPartial(HotelPackageViewModel hotel)
        {
            ViewBag.City = _cityService.GetAllCityOfSelectListItem();
            ViewBag.Hotel = new List<SelectListItem>();
            ViewBag.HotelBoardDDL = _hotelBoardService.GetDDL();
            ViewBag.Tours = _tourService.GetTourDDL();
            var firstTour = _tourService.Filter(x => !x.IsDeleted && x.Recomended).FirstOrDefault();
            ViewBag.TourPackages = new SelectList(_tourPackageService.GetAll().Where(x => !x.IsDeleted && x.TourId == firstTour.Id).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");

            return PartialView("~/Areas/Admin/Views/HotelPackage/_PrvHotel.cshtml", hotel);
        }
        #endregion

        #region GetRoomTypePartial
        [HttpGet]
        public PartialViewResult GetRoomTypePartial(HotelPackageViewModel hotel)
        {
            ViewBag.OtherCurrencyDDL = _currencyService.GetAllCurrenciesOfSelectListItem();
            ViewBag.RoomTypeDDL = _hotelRoomService.GetAllHotelRoomsOfSelectListItem();
            ViewBag.Tours = _tourService.GetTourDDL();
            var firstTour = _tourService.Filter(x => !x.IsDeleted && x.Recomended).FirstOrDefault();
            ViewBag.TourPackages = new SelectList(_tourPackageService.GetAll().Where(x => !x.IsDeleted && x.TourId == firstTour.Id).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");
            return PartialView("~/Areas/Admin/Views/HotelPackage/_PrvRoomType.cshtml", hotel);
        }
        #endregion

        #region Edit
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">شناسه ی پکیج هتل</param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            ViewBag.City = _cityService.GetAllCityOfSelectListItem();
            ViewBag.Hotel = _hotelService.GetAllHotelsOfSelectListItem();//new List<SelectListItem>();
            ViewBag.OtherCurrencyDDL = _currencyService.GetAllCurrenciesOfSelectListItem();
            ViewBag.RoomTypeDDL = _hotelRoomService.GetAllHotelRoomsOfSelectListItem();
            ViewBag.HotelBoardDDL = _hotelBoardService.GetDDL();
            ViewBag.Tours = _tourService.GetTourDDL();
            var firstTour = _tourService.Filter(x => !x.IsDeleted && x.Recomended).FirstOrDefault();
            ViewBag.TourPackages = new SelectList(_tourPackageService.GetAll().Where(x => !x.IsDeleted && x.TourId == firstTour.Id).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");

            var hotelPackage = _hotelPackageService.GetById(hp => hp.Id == id);
            var tourPackage = _tourPackageService.GetById(t => t.Id == hotelPackage.TourPackageId);
            var editHotelPackageVM = new HotelPackageViewModel()
            {
                TourId = tourPackage.TourId,
                TourPackageId = id,
                CRUDMode = CRUDMode.Update,
                OrderId = hotelPackage.OrderId,
                SectionId = SectionId + hotelPackage.Id.ToString(),

                HotelRoomsInPackage = hotelPackage.HotelPackageHotelRooms.Select(x => x.HotelRoom).OrderBy(x => x.Priority).Select(x => new HotelRoomsInPackageViewModel
                {
                    Id = x.Id,
                    AdultPrice = x.HotelPackageHotelRooms.FirstOrDefault(s => s.HotelRoomId == x.Id && s.HotelPackageId == id).AdultPrice,
                    AdultCapacity = x.HotelPackageHotelRooms.FirstOrDefault(s => s.HotelRoomId == x.Id && s.HotelPackageId == id).AdultCapacity,
                    AdultOtherCurrencyPrice = x.HotelPackageHotelRooms.FirstOrDefault(s => s.HotelRoomId == x.Id && s.HotelPackageId == id).AdultOtherCurrencyPrice,                   

                    ChildPrice = x.HotelPackageHotelRooms.FirstOrDefault(s => s.HotelRoomId == x.Id && s.HotelPackageId == id).ChildPrice,
                    ChildCapacity = x.HotelPackageHotelRooms.FirstOrDefault(s => s.HotelRoomId == x.Id && s.HotelPackageId == id).ChildCapacity,
                    ChildOtherCurrencyPrice = x.HotelPackageHotelRooms.FirstOrDefault(s => s.HotelRoomId == x.Id && s.HotelPackageId == id).ChildOtherCurrencyPrice,

                    InfantPrice = x.HotelPackageHotelRooms.FirstOrDefault(s => s.HotelRoomId == x.Id && s.HotelPackageId == id).InfantPrice,
                    InfantCapacity = x.HotelPackageHotelRooms.FirstOrDefault(s => s.HotelRoomId == x.Id && s.HotelPackageId == id).InfantCapacity,                   
                    InfantOtherCurrencyPrice = x.HotelPackageHotelRooms.FirstOrDefault(s => s.HotelRoomId == x.Id && s.HotelPackageId == id).InfantOtherCurrencyPrice,

                    OtherCurrencyId = x.HotelPackageHotelRooms.FirstOrDefault(s => s.HotelRoomId == x.Id && s.HotelPackageId == id).OtherCurrencyId,
                    Title = x.Title,
                    RoomTypeId = x.Id,
                }).ToList(),

                HotelsInPackage = hotelPackage.HotelPackageHotels.Select(x => new { hotel = x.Hotel, hotelBourdId = x.HotelBoardId }).Select(h => new HotelsInPackageViewModel()
                {
                    CityId = h.hotel.CityId,
                    HotelId = h.hotel.Id,
                    HotelBoardId = h.hotelBourdId

                }).ToList(),

                ListView = tourPackage.HotelPackages.Where(x => !x.IsDeleted).Any() ? tourPackage.HotelPackages.Where(x => !x.IsDeleted).Select(tourPackages => GetListView(tourPackages)).ToList() : null
            };

            return PartialView("_AddHotelPackage", editHotelPackageVM);
        }

        [HttpPost]
        public ActionResult Edit(HotelPackageViewModel viewModel)
        {
            ViewBag.OtherCurrencyDDL = _currencyService.GetAllCurrenciesOfSelectListItem();
            ViewBag.RoomTypeDDL = _hotelRoomService.GetAllHotelRoomsOfSelectListItem();
            ViewBag.HotelBoardDDL = _hotelBoardService.GetDDL();
            ViewBag.Tours = _tourService.GetTourDDL();
            var firstTour = _tourService.Filter(x => !x.IsDeleted && x.Recomended).FirstOrDefault();
            ViewBag.TourPackages = new SelectList(_tourPackageService.GetAll().Where(x => !x.IsDeleted && x.TourId == firstTour.Id).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");
            if (ModelState.IsValid)
            {
                var updateHotelPackage = _hotelPackageService.UpdateHotelPackageWithRoomPrice(viewModel);
                return PartialView("_ListViewHotelPackage", GetListView(updateHotelPackage));
            }
            return null;
        }
        #endregion

        #region Delete
        public JsonResult Delete(int id, string sectionId, int tourId)
        {
            bool success = false;
            var model = _hotelPackageService.DeleteLogically(x => x.Id == id);
            if (model.IsDeleted)
            {
                success = true;
                return Json(new { Success = success, SectionId = sectionId, TourId = tourId }, JsonRequestBehavior.DenyGet);
            }
            return Json(success, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region EditHotelRoomInPackage
        public ActionResult EditHotelRoomInPackage()
        {
            return View();
        }

        public JsonResult GetHotelRoomInPackageTable([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            #region Fetch Data
            var query = _hotelPackageService.GetHotelRoomInPackageTable();
            #endregion

            #region Filtering
            // Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                int n;
                bool isNumeric = int.TryParse(value, out n);
                if (isNumeric)
                {
                    var _orderId = Int32.Parse(value);
                    query = query.Where((x => (x.Id != null && x.Id.Equals(_orderId)) ||
                                               (x.TourPackageId != null && x.TourPackageId.Equals(_orderId)) ||
                                               (x.HotelPackageId != null && x.HotelPackageId.Equals(_orderId)) ||
                                               (x.HotelPackageHotelRoomId != null && x.HotelPackageHotelRoomId.Equals(_orderId))));
                }
                else
                {
                    query = query.AsEnumerable().Where((x => (x.TourTitle != null && x.TourTitle.Contains(value)) ||
                                              (x.TourPackageTitle != null && x.TourPackageTitle.Contains(value)) ||
                                              (x.HotelPackageTitleString != null && x.HotelPackageTitleString.Contains(value)) ||
                                              (x.HotelRoomTitle != null && x.HotelRoomTitle.Contains(value))
                                              )).AsQueryable();
                }
            }
            var filteredCount = query.Count();
            #endregion

            #region Sorting
            // Sorting
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var sortColumn = String.Empty;

            foreach (var column in sortedColumns)
            {
                sortColumn += sortColumn != String.Empty ? "," : "";
                sortColumn += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
            }

            query = System.Linq.Dynamic.DynamicQueryable.OrderBy(query, sortColumn.Contains("TourId") ? "HotelPackageId, CreatorDateTime asc" : sortColumn);
            #endregion

            #region Paging
            // Paging
            var data = query.Skip(requestModel.Start).Take(requestModel.Length).ToList();
            #endregion

            #region DataTablesResponse
            var totalCount = query.Count();
            var dataTablesResponse = new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount);
            #endregion

            return Json(dataTablesResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region updatePackagevalue
        public string updatePackagevalue()
        {
            NameValueCollection data = Request.Form;
            string keys = data.Keys[1];
            string val = data[1];
            var dic = (keys.Replace("data", "").Replace("][", ",").Replace("[", "").Replace("]", "")).Split(',').ToArray();
            int id = Int32.Parse(dic[0]);
            string _property = dic[1];
            var updatedrow = _hotelPackageService.InlineUpdate(id, _property, val);
            var row = _hotelPackageService.GetHotelRoomInPackageTable().Where(x => x.HotelPackageHotelRoomId == id).ToList();
            string rowitem = Newtonsoft.Json.JsonConvert.SerializeObject(row);
            rowitem = rowitem.Insert(0, "{\"data\":");
            rowitem = rowitem.Insert(rowitem.Length, "}");
            return rowitem;
        }
        #endregion
    }
}