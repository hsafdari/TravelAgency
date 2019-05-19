using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ParvazPardaz.ViewModel;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Book;
using AutoMapper;
using DataTables.Mvc;
using ParvazPardaz.Service.Contract.Tour;
using OfficeOpenXml;
using ParvazPardaz.Service.Contract.Country;
using ParvazPardaz.Model.Enum;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class OrderController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;
        private readonly ISelectedRoomService _selectedRoomService;
        private readonly IMappingEngine _mappingEngine;
        private readonly ISelectedFlightService _selectedFlightService;
        private readonly IVehicleTypeService _vehicletypeService;
        private readonly IAirportService _airportService;
        private readonly ISelectedHotelPackageService _selectedHotelPackageservice;
        private readonly ICityService _cityService;
        private readonly IPassengerService _passengerService;
        #endregion

        #region	Ctor
        public OrderController(IUnitOfWork unitOfWork, IOrderService orderService, ISelectedRoomService selectedRoomService,
                               ISelectedHotelPackageService selectedHotelPackageService, IMappingEngine mappingEngine,
                                ISelectedFlightService selectedFlightService, IVehicleTypeService vehicletypeService, IAirportService airportService,
                                ISelectedHotelPackageService selectedHotelPackageservice,
                                ICityService cityService, IPassengerService passengerService)
        {
            _unitOfWork = unitOfWork;
            _orderService = orderService;
            _selectedRoomService = selectedRoomService;
            _mappingEngine = mappingEngine;
            _selectedFlightService = selectedFlightService;
            _vehicletypeService = vehicletypeService;
            _airportService = airportService;
            _selectedHotelPackageservice = selectedHotelPackageService;
            _cityService = cityService;
            _passengerService = passengerService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "SellsReport", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        [HttpPost]
        
        public JsonResult GetOrderTable([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel,UserProfileType? profiletype, string fromdate, string todate, string reporttype, string calendertype)
        {
            try
            {
                var query = _orderService.GetViewModelForGrid(profiletype, fromdate, todate, calendertype, reporttype);
                var totalCount = query.Count();

                #region Filtering
                // Apply filters for searching
                if (requestModel.Search.Value != string.Empty)
                {
                    var value = requestModel.Search.Value.Trim();
                    int n;
                    bool isNumeric = int.TryParse(value, out n);
                    if (isNumeric)
                    {
                        long _orderId = long.Parse(value);
                        query = query.Where((x => (x.OrderId != null && x.OrderId.Equals(_orderId))));
                    }
                    query = query.Where((x => (x.TourTitle != null && x.TourTitle.Contains(value)) ||
                                              (x.TourCode != null && x.TourCode.Contains(value)) ||
                                              (x.TourPackageCode != null && x.TourPackageCode.Contains(value)) ||
                                              (x.TrackingCode != null && x.TrackingCode.Contains(value))
                                              ));
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

                query = System.Linq.Dynamic.DynamicQueryable.OrderBy(query, sortColumn == string.Empty ? "Id asc" : sortColumn);//sortColumn + " " + sortColumnDirection);    

                #endregion

                // Paging
                var data = query.Skip(requestModel.Start).Take(requestModel.Length).ToList();

                return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
                //throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        //[CustomAuthorize(Permissionitem.List)]
        [Display(Name = "AreBookingReport", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public ActionResult Doing()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetOrderDoing([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string fromdate, string todate, string reporttype, string calendertype)
        {
            try
            {
                var query = _orderService.GetViewModelForGrid(UserProfileType.Personal, fromdate, todate, calendertype, reporttype).Where(x => x.PayExpiredDateTime >= DateTime.Now && x.IsSuccessful == false);
                var totalCount = query.Count();

                #region Filtering
                // Apply filters for searching
                if (requestModel.Search.Value != string.Empty)
                {
                    var value = requestModel.Search.Value.Trim();
                    int n;
                    bool isNumeric = int.TryParse(value, out n);
                    if (isNumeric)
                    {
                        long _orderId = long.Parse(value);
                        query = query.Where((x => (x.OrderId != null && x.OrderId.Equals(_orderId))));
                    }
                    query = query.Where((x => (x.TourTitle != null && x.TourTitle.Contains(value)) ||
                                              (x.TourCode != null && x.TourCode.Contains(value)) ||
                                              (x.TourPackageCode != null && x.TourPackageCode.Contains(value)) ||
                                              (x.TrackingCode != null && x.TrackingCode.Contains(value))
                                              ));
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

                query = System.Linq.Dynamic.DynamicQueryable.OrderBy(query, sortColumn == string.Empty ? "Id asc" : sortColumn);//sortColumn + " " + sortColumnDirection);    

                #endregion

                // Paging
                var data = query.Skip(requestModel.Start).Take(requestModel.Length).ToList();


                return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Create
      
        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddOrderViewModel addOrderViewModel)
        {
            if (ModelState.IsValid)
            {
                var newOrder = await _orderService.CreateAsync<AddOrderViewModel>(addOrderViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addOrderViewModel);
        }
        #endregion

        #region Edit
        public async Task<ActionResult> Edit(long id)
        {
            EditOrderViewModel editOrderViewModel = await _orderService.GetViewModelAsync<EditOrderViewModel>(x => x.Id == id);
            return View(editOrderViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditOrderViewModel editOrderViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _orderService.UpdateAsync<EditOrderViewModel>(editOrderViewModel, t => t.Id == editOrderViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editOrderViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<JsonResult> Delete(long id)
        {
            var model = await _orderService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

        public ActionResult GetHotelPackages(int Id)
        {
            GridOrderViewModel OrderViewModel = new GridOrderViewModel();

            var Order = _orderService.Filter(x => x.Id == Id).FirstOrDefault();

            OrderViewModel.TourReserve = _mappingEngine.DynamicMap<TourReserveViewModel>(Order);
            //لیست اتاق های انتخابی کاربر
            //selectedHotel.SelectedRoom.Add(new SelelectedRoomInPackageViewModel()
            //{
            //});
            return PartialView("_PrvListOfHotelPackage", OrderViewModel);
        }
        //[CustomAuthorize(Permissionitem.List)]
        [Display(Name = "sellesFlightReports", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public ActionResult OrderFlights()
        {
            ViewBag.VehicleTypes = _vehicletypeService.GetAllVehicleTypesOfSelectListItem();
            ViewBag.Airport = _airportService.GetAllAirPortIataOfSelectListItem();
            GridSelectedFlightViewModel model = new GridSelectedFlightViewModel();
            model.SearchViewModel.fromdate = DateTime.Today;
            model.SearchViewModel.todate = DateTime.Today;
            return View(model);
        }

        
        public JsonResult GetOrderFlights([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string fromdate, string todate,
                                                        string reporttype, int? companytranferId, string fromairport, string toairport, string calendertype)
        {
            try
            {
                var query = _selectedFlightService.GetViewModelForGrid(fromdate, todate, reporttype, companytranferId, fromairport, toairport, calendertype);
                var totalCount = query.Count();

                #region Filtering
                // Apply filters for searching
                if (requestModel.Search.Value != string.Empty)
                {
                    var value = requestModel.Search.Value.Trim();
                    int n;
                    bool isNumeric = int.TryParse(value, out n);
                    if (isNumeric)
                    {
                        long _orderId = long.Parse(value);
                        query = query.Where((x => (x.OrderId == null && x.OrderId.Equals(_orderId))));
                    }
                    query = query.Where((x => (x.FlightNo == null && x.FlightNo.Contains(value)) ||
                                              (x.FromIATACode == null && x.FromIATACode.Contains(value)) ||
                                              (x.ToIATACode == null && x.ToIATACode.Contains(value)) ||
                                              (x.AirlineIATACode == null && x.AirlineIATACode.Contains(value))
                                              ));
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

                query = System.Linq.Dynamic.DynamicQueryable.OrderBy(query, sortColumn == string.Empty ? "Id asc" : sortColumn);//sortColumn + " " + sortColumnDirection);    

                #endregion

                // Paging
                var data = query.Skip(requestModel.Start).Take(requestModel.Length).ToList();


                return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

       
        public ActionResult OrderRooms()
        {
            return View();
        }
    
        public HttpResponseBase ExportOrderFlights(FlightSearchParamViewModel model)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Flights");
                var query = _selectedFlightService.GetViewModelForGrid(model.fromdate, model.todate, model.reporttype, model.companytranferId, model.fromairport, model.toairport)
                    .Select(x => new { Airline = x.AirlineIATACode, fromdate = x.FlightDateTime, todate = x.ReturnFlightDateTime, flightnumber = x.FlightNo, Tourcode = x.TourCode, PackageCode = x.TourPackageCode, buyer = x.BuyerTitle, departure = x.ToIATACode, TrackingCode = x.TrackingCode, id = x.Id, OrderId = x.OrderId }).ToList();
                var result = query.Select(x => new
                {
                    x.Airline,
                    fromdate = x.fromdate.ToString("yyyy/MM/dd"),
                    todate = x.todate != null ? x.todate.Value.ToString("yyyy/MM/dd") : "",
                    Gfromdate = x.fromdate.Year+"/"+ x.fromdate.Month+"/"+ x.fromdate.Day,
                    Gtodate = x.todate != null ? x.todate.Value.Year+"/"+ x.todate.Value.Month+"/"+  x.todate.Value.Day: "",
                    x.flightnumber,
                    x.Tourcode,
                    x.PackageCode,
                    x.buyer,
                    x.departure,
                    x.TrackingCode,
                    x.id,
                    x.OrderId

                });
                ws.Cells["A1"].Value = "نوع ایرلاین";
                ws.Cells["B1"].Value = "از تاریخ شمسی";
                ws.Cells["C1"].Value = "تا تاریخ شمسی";
                ws.Cells["D1"].Value = "از تاریخ میلادی";
                ws.Cells["E1"].Value = "تا تاریخ میلادی";
                ws.Cells["F1"].Value = "شماره پرواز";
                ws.Cells["G1"].Value = "کد تور";
                ws.Cells["H1"].Value = "کد پکیج";
                ws.Cells["I1"].Value = "نام خریدار (نام کاربری)";
                ws.Cells["J1"].Value = "مقصد";
                ws.Cells["K1"].Value = "کد پیگیری";
                ws.Cells["A2"].LoadFromCollection(result);

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment; filename=Flights" + query.Select(x => x.id).FirstOrDefault() + ".xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();
                return Response;
            }
        }
        public ActionResult OrderHotels()
        {
            GridSelectedHotelPackageViewModel model = new GridSelectedHotelPackageViewModel();
            ViewBag.cityId = _cityService.GetAllCityOfSelectListItem();
            model.SearchViewModel.fromdate = DateTime.Today;
            model.SearchViewModel.todate = DateTime.Today;
            return View(model);
        }

        
        public JsonResult GetOrderHotels([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string fromdate, string todate, string reporttype, int? cityId, int? hotelId, string calendertype)
        {
            //try
            //{
            var query = _selectedHotelPackageservice.GetViewModelForGrid(fromdate, todate, reporttype, cityId, hotelId, calendertype);
            var totalCount = query.Count();

            #region Filtering
            // Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                int n;
                bool isNumeric = int.TryParse(value, out n);
                if (isNumeric)
                {
                    long _orderId = long.Parse(value);
                    query = query.Where((x => (x.OrderId == null && x.OrderId.Equals(_orderId))));
                }
                query = query.Where((x => (x.BuyerTitle == null && x.BuyerTitle.Contains(value)) ||
                                          (x.NationalCode == null && x.NationalCode.Contains(value)) ||
                                          (x.TourCode == null && x.TourCode.Contains(value)) ||
                                          (x.TourPackageCode == null && x.TourPackageCode.Contains(value)) ||
                                          (x.TourPackageTitle == null && x.TourPackageTitle.Contains(value)) ||
                                          (x.TourTitle == null && x.TourTitle.Contains(value)) ||
                                          (x.TrackingCode == null && x.TrackingCode.Contains(value))
                                          ));
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

            query = System.Linq.Dynamic.DynamicQueryable.OrderBy(query, sortColumn == string.Empty ? "Id asc" : sortColumn);//sortColumn + " " + sortColumnDirection);    

            #endregion

            // Paging
            var data = query.Skip(requestModel.Start).Take(requestModel.Length).ToList();


            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception ex)
            //{

            //    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //    return Json("error", JsonRequestBehavior.AllowGet);
            //}
        }
        [HttpPost]
        public HttpResponseBase ExportOrderHotels(HotelSearchParamViewModel model)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Hotels");
                var query = _selectedHotelPackageservice.GetViewModelForGrid(model.fromdate, model.todate, model.reporttype, model.cityId, model.hotelId)
                    .Select(x => new  { Id = x.Id, fromdate = x.FlightDateTime, todate = x.ReturnFlightDateTime, Hotel = x.hotelsInPackage, numberroom = x.NumberRoom, BedType = x.hotelRoomsInPackage, OrderId = x.OrderId, TotalPay = x.TotalPayPrice, Buyer = x.BuyerTitle }).ToList();
                List<ExportExcelSelectedHotelViewModel> export = new List<ExportExcelSelectedHotelViewModel>();
                foreach (var item in query)
                {
                    ExportExcelSelectedHotelViewModel excelitem = new ExportExcelSelectedHotelViewModel();
                    excelitem.Id = item.Id;
                    excelitem.fromdate = item.fromdate.ToString("yyyy/MM/dd");
                    excelitem.todate = item.todate != null ? item.todate.Value.ToString("yyyy/MM/dd") : "";
                    excelitem.Gfromdate = item.fromdate.Year + "/" + item.fromdate.Month + "/" + item.fromdate.Day;
                    excelitem.Gtodate = item.todate != null ? item.todate.Value.Year + "/" + item.todate.Value.Month + "/" + item.todate.Value.Day : "";
                    excelitem.Hotel = string.Join(",", item.Hotel.Select(y => y.HotelTitle));
                    excelitem.City = string.Join(",", item.Hotel.Select(y => y.CityTitle));
                    excelitem.numberroom=item.numberroom;
                    excelitem.BedType = string.Join(",", item.BedType.Select(y => y.Title));
                    excelitem.OrderId=item.OrderId;
                    excelitem.TotalPay=item.TotalPay;
                    excelitem.Buyer=item.Buyer;
                    export.Add(excelitem);
                }
                ws.Cells["A1"].Value = "";
                ws.Cells["B1"].Value = "از تاریخ شمسی";
                ws.Cells["C1"].Value = "تا تاریخ شمسی";
                ws.Cells["D1"].Value = "از تاریخ میلادی";
                ws.Cells["E1"].Value = "تا تاریخ میلادی";
                ws.Cells["F1"].Value = "نام هتل";
                ws.Cells["G1"].Value = "نام شهر";
                ws.Cells["H1"].Value = "تعداد افراد";
                ws.Cells["I1"].Value = "نوع تخت";
                ws.Cells["J1"].Value = "کد فروش";
                ws.Cells["K1"].Value = "مبلغ فروش";
                ws.Cells["L1"].Value = "نام خریدار";
                ws.Cells["A2"].LoadFromCollection(export);

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment; filename=Hotels" + query.Select(x => x.Id).FirstOrDefault() + ".xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();
                return Response;
            }
        }
       // [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "sellesPassengerReports", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public ActionResult OrderPassengers()
        {
            GridPassengerViewModel model = new GridPassengerViewModel();
            model.SearchViewModel.fromdate = DateTime.Today;
            model.SearchViewModel.todate = DateTime.Today;
            return View(model);
        }

        
        public ActionResult GetOrderPassengers([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string fromdate, string todate, string reporttype, string calendertype)
        {
            try
            {
                var query = _passengerService.GetViewModelForGrid(fromdate, todate, reporttype, calendertype);
                var totalCount = query.Count();

                #region Filtering
                // Apply filters for searching
                if (requestModel.Search.Value != string.Empty)
                {
                    var value = requestModel.Search.Value.Trim();
                    int n;
                    bool isNumeric = int.TryParse(value, out n);
                    if (isNumeric)
                    {
                        long _orderId = long.Parse(value);
                        query = query.Where((x => (x.OrderId == null && x.OrderId.Equals(_orderId))));
                    }
                    query = query.Where((x => (x.AgeRangeTitle == null && x.AgeRangeTitle.Contains(value)) ||
                                              (x.BirthCountry == null && x.BirthCountry.Contains(value)) ||
                                              (x.BuyerTitle == null && x.BuyerTitle.Contains(value)) ||
                                              (x.CreatorUserName == null && x.CreatorUserName.Contains(value)) ||
                                              (x.CurrencyTitle == null && x.CurrencyTitle.Contains(value)) ||
                                              (x.EnFirstName == null && x.EnFirstName.Contains(value)) ||
                                              (x.EnLastName == null && x.EnLastName.Contains(value)) ||
                                              (x.FirstName == null && x.FirstName.Contains(value)) ||
                                              (x.LastName == null && x.LastName.Contains(value)) ||
                                              (x.NationalCode == null && x.NationalCode.Contains(value)) ||
                                              (x.PassportExporterCountry == null && x.PassportExporterCountry.Contains(value)) ||
                                              (x.PassportNo == null && x.PassportNo.Contains(value)) ||
                                              (x.TrackingCode == null && x.TrackingCode.Contains(value)) ||
                                              (x.Nationality == null && x.Nationality.Contains(value))
                                              ));
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

                query = System.Linq.Dynamic.DynamicQueryable.OrderBy(query, sortColumn == string.Empty ? "Id asc" : sortColumn);//sortColumn + " " + sortColumnDirection);    

                #endregion

                // Paging
                var data = query.AsEnumerable().Skip(requestModel.Start).Take(requestModel.Length).ToList();
                return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        public HttpResponseBase ExportOrderPassengers(PassengerSearchParamViewModel model)
        {
            throw new NotImplementedException();
        }
        
    }
}
